using Microsoft.EntityFrameworkCore;
using GestaoResiduosApi.Models;

namespace GestaoResiduosApi.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CaminhaoModel> Caminhoes { get; set; }

        public virtual DbSet<RecipienteModel> Recipiente { get; set; }

        public virtual DbSet<RotaModel> Rota { get; set; }

        public virtual DbSet<AgendamentoModel> Agendamento { get; set; }

        public virtual DbSet<EmergenciaModel> Emergencia { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaminhaoModel>(entity =>
            {
                entity.ToTable("t_caminhao"); // Nome da tabela
                entity.HasKey(e => e.IdCaminhao); // Chave primária

                entity.Property(e => e.IdCaminhao)
                      .HasColumnName("id_caminhao")
                      .ValueGeneratedOnAdd(); // Gerado pelo banco (Identity)

                entity.Property(e => e.Placa)
                      .HasColumnName("ds_placa")
                      .IsRequired()
                      .HasMaxLength(255); // Limite de tamanho

                entity.Property(e => e.Capacidade)
                      .HasColumnName("ds_capacidade")
                      .IsRequired(); // Capacidade como obrigatório
            });

            modelBuilder.Entity<RecipienteModel>(entity =>
            {
                entity.ToTable("t_recipiente"); // Nome da tabela
                entity.HasKey(e => e.IdRecipiente); // Chave primária

                entity.Property(e => e.IdRecipiente)
                      .HasColumnName("id_recipiente")
                      .ValueGeneratedOnAdd(); // Geração automática pelo banco

                entity.Property(e => e.MaxCapacidade)
                      .HasColumnName("max_capacidade")
                      .IsRequired();

                entity.Property(e => e.AtualNivel)
                      .HasColumnName("atual_nvl")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .HasConversion<string>() // Armazena como texto
                      .IsRequired();

                entity.Property(e => e.UltimaAtualizacao)
                      .HasColumnName("ultima_att")
                      .HasColumnType("date") // Formata para `date` no banco
                      .IsRequired();

                entity.Property(e => e.Latitude)
                      .HasColumnName("latitude_recip")
                      .IsRequired();

                entity.Property(e => e.Longitude)
                      .HasColumnName("longitude_recip")
                      .IsRequired();
            });

            modelBuilder.Entity<RotaModel>(entity =>
            {
                entity.ToTable("t_rota"); // Nome da tabela no banco
                entity.HasKey(e => e.IdRota); // Chave primária

                entity.Property(e => e.IdRota)
                      .HasColumnName("id_rota")
                      .ValueGeneratedOnAdd(); // Geração automática pelo banco

                entity.Property(e => e.DtRota)
                      .HasColumnName("dt_rota")
                      .HasColumnType("date") // Configura como `date`
                      .IsRequired();

                entity.HasOne(e => e.Caminhao)
                      .WithMany() // Um caminhão pode ter várias rotas
                      .HasForeignKey("T_CAMINHAO_id_caminhao") // Nome da FK
                      .IsRequired();

                entity.HasOne(e => e.Recipiente)
                      .WithMany() // Um recipiente pode estar associado a várias rotas
                      .HasForeignKey("T_RECIPIENTE_id_recipiente") // Nome da FK
                      .IsRequired();
            });

            modelBuilder.Entity<AgendamentoModel>(entity =>
            {
                entity.ToTable("t_agendamento"); // Nome da tabela no banco
                entity.HasKey(e => e.IdAgendamento); // Chave primária

                entity.Property(e => e.IdAgendamento)
                      .HasColumnName("id_agendamento")
                      .ValueGeneratedOnAdd(); // Geração automática pelo banco

                entity.Property(e => e.DtAgendamento)
                      .HasColumnName("dt_agendamento")
                      .HasColumnType("date") // Configura como `date`
                      .IsRequired();

                entity.Property(e => e.StatusAgendamento)
                      .HasColumnName("status_agendamento")
                      .HasConversion<string>() // Armazena o enum como string
                      .IsRequired();

                entity.HasOne(e => e.Rota)
                      .WithMany() // Uma rota pode ter vários agendamentos
                      .HasForeignKey("T_ROTA_id_rota") // Nome da FK
                      .IsRequired();
            });

            modelBuilder.Entity<EmergenciaModel>(entity =>
            {
                entity.ToTable("t_emergencia"); // Nome da tabela no banco
                entity.HasKey(e => e.IdEmergencia); // Chave primária

                entity.Property(e => e.IdEmergencia)
                      .HasColumnName("id_emergencia")
                      .ValueGeneratedOnAdd(); // Geração automática pelo banco

                entity.Property(e => e.DtEmergencia)
                      .HasColumnName("dt_emergencia")
                      .HasColumnType("date") // Configura como `date`
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .HasConversion<string>() // Armazena o enum como string
                      .IsRequired();

                entity.Property(e => e.Descricao)
                      .HasColumnName("ds_emergencia")
                      .HasMaxLength(500) // Limita o tamanho da descrição
                      .IsRequired();

                entity.HasOne(e => e.Recipiente)
                      .WithMany() // Um recipiente pode ter várias emergências
                      .HasForeignKey("T_RECIPIENTE_ID_RECIPIENTE") // Nome da FK
                      .IsRequired();

                entity.HasOne(e => e.Caminhao)
                      .WithMany() // Um caminhão pode ter várias emergências
                      .HasForeignKey("T_CAMINHAO_ID_CAMINHAO") // Nome da FK
                      .IsRequired();
            });

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("t_usuario"); // Nome da tabela

                entity.HasKey(e => e.UsuarioId); // Chave primária

                entity.Property(e => e.UsuarioId)
                      .HasColumnName("usuario_id")
                      .ValueGeneratedOnAdd(); // Geração automática

                entity.Property(e => e.Nome)
                      .HasColumnName("nome")
                      .IsRequired()
                      .HasMaxLength(255); // Limite de tamanho

                entity.Property(e => e.Email)
                      .HasColumnName("email")
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Senha)
                      .HasColumnName("senha")
                      .IsRequired();

                entity.Property(e => e.Role)
                      .HasColumnName("role")
                      .HasConversion<string>() // Enum armazenado como string
                      .IsRequired();
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
