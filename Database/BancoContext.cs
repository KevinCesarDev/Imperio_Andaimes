using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Models;

namespace TCC.Database;

public class BancoContext : DbContext
{
    public BancoContext(DbContextOptions<BancoContext> options) : base(options){}

    public DbSet<EstoqueModel> EstoqueGeral { get; set; }
    public DbSet<PedidoModel> Pedidos { get; set; }
    public DbSet<NotaModel> Notas { get; set; }
    public DbSet<UsuarioModel> Usuarios{get;set;}
    public DbSet<ContaModel> Contas {get;set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstoqueModel>(ConfigureEstoqueModel);
        
        modelBuilder.Entity<ContaModel>().HasData( new ContaModel {Id = 1,TipoConta = "Administrador"},new ContaModel {Id = 2,TipoConta = "Funcionario"});
        modelBuilder.Entity<UsuarioModel>().HasData( new UsuarioModel {Id = 1,EmailUsuario = "koio@email.com",Senha = "123",NomeUsuario = "Koios", IdConta = 1});
    }

    private void ConfigureEstoqueModel(EntityTypeBuilder<EstoqueModel> builder)
    {
        builder.Property(e => e.ProdutoAtivo)
            .HasDefaultValue(true);
    }

}