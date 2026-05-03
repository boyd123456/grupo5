using Microsoft.Data.SqlClient;
using PlataExpress.Models;

namespace PlataExpress.Data
{
    public class RemesaRepository
    {
        private readonly string _connectionString;

        public RemesaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DineroConnection");
        }

        public async Task<bool> EnviarRemesaAsync(Remesa remesa)
        {
            try
            {
                var sql = @"INSERT INTO Remesas
                            (IdUsuario, TipoOperacion, Nombre, Agencia, Monto, Comision, FechaEnvio, Estado)
                            VALUES
                            (@IdUsuario, @TipoOperacion, @Nombre, @Agencia, @Monto, @Comision, @FechaEnvio, @Estado)";

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@IdUsuario", System.Data.SqlDbType.Int).Value = remesa.IdUsuario;
                    cmd.Parameters.Add("@TipoOperacion", System.Data.SqlDbType.VarChar).Value = remesa.TipoOperacion;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = remesa.Nombre;
                    cmd.Parameters.Add("@Agencia", System.Data.SqlDbType.VarChar).Value = remesa.Agencia;
                    cmd.Parameters.Add("@Monto", System.Data.SqlDbType.Decimal).Value = remesa.Monto;
                    cmd.Parameters.Add("@Comision", System.Data.SqlDbType.Decimal).Value = remesa.Comision;
                    cmd.Parameters.Add("@FechaEnvio", System.Data.SqlDbType.DateTime).Value = remesa.FechaEnvio;
                    cmd.Parameters.Add("@Estado", System.Data.SqlDbType.VarChar).Value = remesa.Estado;

                    await conn.OpenAsync();
                    int filas = await cmd.ExecuteNonQueryAsync();

                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR AL GUARDAR REMESA: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Remesa>> ObtenerRemesasPorUsuario(int idUsuario)
        {
            var lista = new List<Remesa>();

            var sql = @"
                    SELECT 
                r.IdRemesa,
                r.IdUsuario,
                r.TipoOperacion,
                r.Nombre,
                r.Agencia,
                r.Monto,
                r.Comision,
                r.FechaEnvio,
                r.Estado,
                (u.Nombres + ' ' + u.Apellidos) AS NombreUsuario
            FROM Remesas r
            INNER JOIN Usuarios u ON r.IdUsuario = u.IdUsuario
            WHERE r.IdUsuario = @IdUsuario
            ORDER BY r.FechaEnvio DESC
            ";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@IdUsuario", System.Data.SqlDbType.Int).Value = idUsuario;

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Remesa
                        {
                            IdRemesa = reader.GetInt32(0),
                            IdUsuario = reader.GetInt32(1),
                            TipoOperacion = reader.GetString(2),
                            Nombre = reader.GetString(3),
                            Agencia = reader.GetString(4),
                            Monto = reader.GetDecimal(5),
                            Comision = reader.GetDecimal(6),
                            FechaEnvio = reader.GetDateTime(7),
                            Estado = reader.GetString(8),
                            NombreUsuario = reader.GetString(9)
                        });
                    }
                }
            }

            return lista;
        }
        // 🔢 Total de envíos
        public async Task<int> ObtenerTotalEnvios(int idUsuario)
        {
            var sql = "SELECT COUNT(*) FROM Remesas WHERE IdUsuario = @IdUsuario";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync();
        }

        // 💰 Total enviado
        public async Task<decimal> ObtenerTotalMonto(int idUsuario)
        {
            var sql = "SELECT ISNULL(SUM(Monto),0) FROM Remesas WHERE IdUsuario = @IdUsuario";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            return (decimal)await cmd.ExecuteScalarAsync();
        }

        // ⏳ En proceso
        public async Task<int> ObtenerEnProceso(int idUsuario)
        {
            var sql = "SELECT COUNT(*) FROM Remesas WHERE IdUsuario = @IdUsuario AND Estado = 'Enviado'";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync();
        }
        // 📄 Últimos envíos
        public async Task<List<Remesa>> ObtenerUltimosEnvios(int idUsuario)
        {
            var lista = new List<Remesa>();

            var sql = @"SELECT TOP 5
                    r.Nombre,
                    r.Agencia,
                    r.Monto,
                    r.Estado,
                    (u.Nombres + ' ' + u.Apellidos) AS NombreUsuario
                FROM Remesas r
                INNER JOIN Usuarios u ON r.IdUsuario = u.IdUsuario
                WHERE r.IdUsuario = @IdUsuario
                ORDER BY r.FechaEnvio DESC";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Remesa
                {
                    Nombre = reader["Nombre"].ToString(),
                    Agencia = reader["Agencia"].ToString(),
                    Monto = Convert.ToDecimal(reader["Monto"]),
                    Estado = reader["Estado"].ToString(),
                    NombreUsuario = reader["NombreUsuario"].ToString()
                });
            }

            return lista;
        }
        public async Task<int?> ObtenerIdUsuarioPorUsuario(string usuario)
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = "SELECT IdUsuario FROM Usuarios WHERE Usuario = @Usuario";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Usuario", usuario);

            await conn.OpenAsync();

            var result = await cmd.ExecuteScalarAsync();

            return result != null ? (int?)Convert.ToInt32(result) : null;
        }

        public async Task TransferirDinero(int origen, int destino, decimal monto)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();

            try
            {
                // Restar saldo
                var cmd1 = new SqlCommand(
                    "UPDATE Usuarios SET Saldo = Saldo - @Monto WHERE IdUsuario = @Origen",
                    conn, transaction);

                cmd1.Parameters.AddWithValue("@Monto", monto);
                cmd1.Parameters.AddWithValue("@Origen", origen);
                await cmd1.ExecuteNonQueryAsync();

                // Sumar saldo
                var cmd2 = new SqlCommand(
                    "UPDATE Usuarios SET Saldo = Saldo + @Monto WHERE IdUsuario = @Destino",
                    conn, transaction);

                cmd2.Parameters.AddWithValue("@Monto", monto);
                cmd2.Parameters.AddWithValue("@Destino", destino);
                await cmd2.ExecuteNonQueryAsync();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task<decimal> ObtenerSaldo(int idUsuario)
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = "SELECT Saldo FROM Usuarios WHERE IdUsuario = @IdUsuario";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();

            var result = await cmd.ExecuteScalarAsync();

            return result != null ? Convert.ToDecimal(result) : 0;
        }

        public async Task RecargarSaldo(int idUsuario, decimal monto)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();

            try
            {
                // 💰 sumar saldo
                var cmd = new SqlCommand(
                    "UPDATE Usuarios SET Saldo = Saldo + @Monto WHERE IdUsuario = @IdUsuario",
                    conn, transaction);

                cmd.Parameters.AddWithValue("@Monto", monto);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                await cmd.ExecuteNonQueryAsync();

                // 🧾 guardar historial (opcional)
                var cmd2 = new SqlCommand(
                    "INSERT INTO Recargas (IdUsuario, Monto) VALUES (@IdUsuario, @Monto)",
                    conn, transaction);

                cmd2.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd2.Parameters.AddWithValue("@Monto", monto);

                await cmd2.ExecuteNonQueryAsync();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}

