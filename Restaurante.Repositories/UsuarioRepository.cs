using Dapper;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using System.Data;

namespace Restaurante.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _db;

    public UsuarioRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task AddAsync(Usuario usuario)
    {
        var sql = @"INSERT INTO Usuarios (email, senha, data_criacao, criado_por) 
                    VALUES (@email, @senha, @data_criacao, @criado_por)";

        var parametros = new DynamicParameters();
        parametros.Add("@email", usuario.Email, DbType.String);
        parametros.Add("@senha", usuario.Senha, DbType.String);
        parametros.Add("@data_criacao", usuario.DataCriacao, DbType.DateTime);
        parametros.Add("@criado_por", usuario.CriadoPor, DbType.String);

        await _db.ExecuteAsync(sql, parametros);
    }

    public async Task DeleteAsync(int id, string atualizadoPor)
    {
        var sql = @"UPDATE Usuarios 
                    SET ativo = @Ativo,
                        atualizado_por = @AtualizadoPor, 
                        data_atualizacao = @DataAtualizacao  
                    WHERE Id = @Id";

        var parametros = new DynamicParameters();
        parametros.Add("@Ativo", false, DbType.Boolean);
        parametros.Add("@Id", id, DbType.Int32);
        parametros.Add("@AtualizadoPor", atualizadoPor, DbType.String);
        parametros.Add("@DataAtualizacao", DateTime.Now, DbType.DateTime);

        await _db.ExecuteAsync(sql, parametros);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var sql = @"SELECT 
                        Id, 
                        Email,
                        Senha,
                        Data_Criacao AS DataCriacao,
                        Data_Atualizacao AS DataAtualizacao,
                        Criado_Por AS CriadoPor,
                        Atualizado_Por AS AtualizadoPor,
                        Ativo
                    FROM Usuarios 
                    WHERE ativo = 1";

        var result = await _db.QueryAsync<Usuario>(sql);
        return result.ToList();
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        var sql = @"SELECT 
                        Id, 
                        Email,
                        Senha,
                        Data_Criacao AS DataCriacao,
                        Data_Atualizacao AS DataAtualizacao,
                        Criado_Por AS CriadoPor,
                        Atualizado_Por AS AtualizadoPor,
                        Ativo
                    FROM Usuarios 
                    WHERE Email = @Email AND ativo = 1";

        var parametros = new DynamicParameters();
        parametros.Add("@Email", email, DbType.String);

        return await _db.QueryFirstOrDefaultAsync<Usuario>(sql, parametros);

    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        var sql = @"SELECT 
                        Id, 
                        Email,
                        Senha,
                        Data_Criacao AS DataCriacao,
                        Data_Atualizacao AS DataAtualizacao,
                        Criado_Por AS CriadoPor,
                        Atualizado_Por AS AtualizadoPor,
                        Ativo
                    FROM Usuarios 
                    WHERE Id = @Id AND ativo = 1";

        var parametros = new DynamicParameters();
        parametros.Add("@Id", id, DbType.Int32);

        return await _db.QueryFirstOrDefaultAsync<Usuario>(sql, parametros);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        var sql = @"UPDATE Usuarios 
                    SET 
                    Ativo = @Ativo,
                    Email = @Email,
                    Senha = @Senha,
                    Data_Atualizacao = @DataAtualizacao,
                    Atualizado_Por = @AtualizadoPor
                    WHERE Id = @Id";

        var parametros = new DynamicParameters();
        parametros.Add("@Id", usuario.Id, DbType.Int32);
        parametros.Add("@Ativo", usuario.Ativo, DbType.Boolean);
        parametros.Add("@Email", usuario.Email, DbType.String);
        parametros.Add("@Senha", usuario.Senha, DbType.String);
        parametros.Add("@DataAtualizacao", usuario.DataAtualizacao, DbType.DateTime);
        parametros.Add("@AtualizadoPor", usuario.AtualizadoPor, DbType.String);

        await _db.ExecuteAsync(sql, parametros);
    }
}
