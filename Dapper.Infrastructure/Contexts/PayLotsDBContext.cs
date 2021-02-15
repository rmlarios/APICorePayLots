using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dapper.Core.Model;

namespace Dapper.Infrastructure.Contexts
{
    public partial class PayLotsDBContext : DbContext
    {
        public PayLotsDBContext()
        {
        }

        public PayLotsDBContext(DbContextOptions<PayLotsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignaciones> Asignaciones { get; set; }
        //public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetRoles1> AspNetRoles1 { get; set; }
        //public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        //public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        //public virtual DbSet<AspNetUsers1> AspNetUsers1 { get; set; }
        //public virtual DbSet<AspnetApplications> AspnetApplications { get; set; }
        //public virtual DbSet<AspnetMembership> AspnetMembership { get; set; }
        //public virtual DbSet<AspnetPaths> AspnetPaths { get; set; }
        //public virtual DbSet<AspnetPersonalizationAllUsers> AspnetPersonalizationAllUsers { get; set; }
        //public virtual DbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; }
        //public virtual DbSet<AspnetProfile> AspnetProfile { get; set; }
        //public virtual DbSet<AspnetRoles> AspnetRoles { get; set; }
        //public virtual DbSet<AspnetSchemaVersions> AspnetSchemaVersions { get; set; }
        //public virtual DbSet<AspnetUsers> AspnetUsers { get; set; }
        //public virtual DbSet<AspnetUsersInRoles> AspnetUsersInRoles { get; set; }
        //public virtual DbSet<AspnetWebEventEvents> AspnetWebEventEvents { get; set; }
        public virtual DbSet<Beneficiarios> Beneficiarios { get; set; }
        public virtual DbSet<Bloques> Bloques { get; set; }
        public virtual DbSet<CatalogoDepartamentos> CatalogoDepartamentos { get; set; }
        public virtual DbSet<CatalogoMunicipios> CatalogoMunicipios { get; set; }
        public virtual DbSet<CatalogoPreguntas> CatalogoPreguntas { get; set; }
        //public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<ControlAccesoUsuarios> ControlAccesoUsuarios { get; set; }
        public virtual DbSet<DatosEmpresa> DatosEmpresa { get; set; }
        public virtual DbSet<ErrorSql> ErrorSql { get; set; }
        public virtual DbSet<AbonosPrima> AbonosPrima{ get; set; }
    public virtual DbSet<ErroresSistema> ErroresSistema { get; set; }
        //public virtual DbSet<Importacion> Importacion { get; set; }
        //public virtual DbSet<Importacion2> Importacion2 { get; set; }
        //public virtual DbSet<Importacion3> Importacion3 { get; set; }
        public virtual DbSet<Lotes> Lotes { get; set; }
        public virtual DbSet<Mora> Mora { get; set; }
        public virtual DbSet<Morosos> Morosos { get; set; }
        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<Proformas> Proformas { get; set; }
        public virtual DbSet<Seguimientos> Seguimientos { get; set; }
        public virtual DbSet<Ubicaciones> Ubicaciones { get; set; }
        public virtual DbSet<ViewAsignacionesLotes> ViewAsignacionesLotes { get; set; }
        public virtual DbSet<ViewAsignacionesSaldo> ViewAsignacionesSaldo { get; set; }
        public virtual DbSet<ViewBloquesUbicacion> ViewBloquesUbicacion { get; set; }
        public virtual DbSet<ViewConsolidadoBloques> ViewConsolidadoBloques { get; set; }
        public virtual DbSet<ViewConsolidadoUbicaciones> ViewConsolidadoUbicaciones { get; set; }
        public virtual DbSet<ViewDashboard1> ViewDashboard1 { get; set; }
        public virtual DbSet<ViewDashBoard> ViewDashboard { get; set; }
        public virtual DbSet<ViewAbonosPrima> ViewAbonosPrima { get; set; }
    public virtual DbSet<ViewDepartamentosMunicipios> ViewDepartamentosMunicipios { get; set; }
        public virtual DbSet<ViewGraficoPagos> ViewGraficoPagos { get; set; }
        public virtual DbSet<ViewLotes> ViewLotes { get; set; }
        public virtual DbSet<ViewPagosAsignaciones> ViewPagosAsignaciones { get; set; }
        public virtual DbSet<ViewReporteMorosos> ViewReporteMorosos { get; set; }
        public virtual DbSet<ViewReportePlanPago> ViewReportePlanPago { get; set; }
        public virtual DbSet<ViewReporteTicketPago> ViewReporteTicketPago { get; set; }
        public virtual DbSet<ViewTotalAbonadoAsignaciones> ViewTotalAbonadoAsignaciones { get; set; }
        //public virtual DbSet<ViewUsuariosSistema> ViewUsuariosSistema { get; set; }
        //public virtual DbSet<VwAspnetApplications> VwAspnetApplications { get; set; }
        //public virtual DbSet<VwAspnetMembershipUsers> VwAspnetMembershipUsers { get; set; }
        //public virtual DbSet<VwAspnetProfiles> VwAspnetProfiles { get; set; }
        //public virtual DbSet<VwAspnetRoles> VwAspnetRoles { get; set; }
        //public virtual DbSet<VwAspnetUsers> VwAspnetUsers { get; set; }
        //public virtual DbSet<VwAspnetUsersInRoles> VwAspnetUsersInRoles { get; set; }
        //public virtual DbSet<VwAspnetWebPartStatePaths> VwAspnetWebPartStatePaths { get; set; }
        //public virtual DbSet<VwAspnetWebPartStateShared> VwAspnetWebPartStateShared { get; set; }
        //public virtual DbSet<VwAspnetWebPartStateUser> VwAspnetWebPartStateUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /*  if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MEM-INFO-10\\LOCALSQL2016;Database=PayLots;Trusted_Connection=True;");
            } */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Asignaciones>(entity =>
            {
                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany(p => p.Asignaciones)
                    .HasForeignKey(d => d.IdBeneficiario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asignaciones_Beneficiarios");

                entity.HasOne(d => d.IdLoteNavigation)
                    .WithMany(p => p.Asignaciones)
                    .HasForeignKey(d => d.IdLote)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asignaciones_Lotes");
            });

            /*modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<AspnetApplications>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK__aspnet_A__C93A4C98A944788B")
                    .IsClustered(false);

                entity.HasIndex(e => e.ApplicationName)
                    .HasName("UQ__aspnet_A__30910331D5193993")
                    .IsUnique();

                entity.HasIndex(e => e.LoweredApplicationName)
                    .HasName("UQ__aspnet_A__17477DE473C17A67")
                    .IsUnique();

                entity.Property(e => e.ApplicationId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<AspnetMembership>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__aspnet_M__1788CC4D6D0B4B87")
                    .IsClustered(false);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetMembership)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Me__Appli__6D0D32F4");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspnetMembership)
                    .HasForeignKey<AspnetMembership>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Me__UserI__6EF57B66");
            });

            modelBuilder.Entity<AspnetPaths>(entity =>
            {
                entity.HasKey(e => e.PathId)
                    .HasName("PK__aspnet_P__CD67DC5817BEF13A")
                    .IsClustered(false);

                entity.Property(e => e.PathId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetPaths)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pa__Appli__6FE99F9F");
            });

            modelBuilder.Entity<AspnetPersonalizationAllUsers>(entity =>
            {
                entity.HasKey(e => e.PathId)
                    .HasName("PK__aspnet_P__CD67DC599A9AC319");

                entity.Property(e => e.PathId).ValueGeneratedNever();

                entity.HasOne(d => d.Path)
                    .WithOne(p => p.AspnetPersonalizationAllUsers)
                    .HasForeignKey<AspnetPersonalizationAllUsers>(d => d.PathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pe__PathI__70DDC3D8");
            });

            modelBuilder.Entity<AspnetPersonalizationPerUser>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__aspnet_P__3214EC06DDEA9310")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Path)
                    .WithMany(p => p.AspnetPersonalizationPerUser)
                    .HasForeignKey(d => d.PathId)
                    .HasConstraintName("FK__aspnet_Pe__PathI__71D1E811");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspnetPersonalizationPerUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__aspnet_Pe__UserI__72C60C4A");
            });

            modelBuilder.Entity<AspnetProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__aspnet_P__1788CC4C2F93D8AB");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspnetProfile)
                    .HasForeignKey<AspnetProfile>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Pr__UserI__73BA3083");
            });

            modelBuilder.Entity<AspnetRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__aspnet_R__8AFACE1BB751397C")
                    .IsClustered(false);

                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetRoles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Ro__Appli__74AE54BC");
            });

            modelBuilder.Entity<AspnetSchemaVersions>(entity =>
            {
                entity.HasKey(e => new { e.Feature, e.CompatibleSchemaVersion })
                    .HasName("PK__aspnet_S__5A1E6BC1A7BF36D8");
            });

            modelBuilder.Entity<AspnetUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__aspnet_U__1788CC4D57AEF7FD")
                    .IsClustered(false);

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspnetUsers)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__Appli__75A278F5");
            });

            modelBuilder.Entity<AspnetUsersInRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK__aspnet_U__AF2760AD201C749D");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspnetUsersInRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__RoleI__76969D2E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspnetUsersInRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__aspnet_Us__UserI__778AC167");
            });

            modelBuilder.Entity<AspnetWebEventEvents>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__aspnet_W__7944C810FE03D5B7");

                entity.Property(e => e.EventId)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            */

            modelBuilder.Entity<Bloques>(entity =>
            {
                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.Bloques)
                    .HasForeignKey(d => d.IdUbicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bloques_Ubicaciones");
            });

            modelBuilder.Entity<CatalogoMunicipios>(entity =>
            {
                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.CatalogoMunicipios)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Catalogo_Municipios_Catalogo_Departamentos");
            });

           /*  modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasNoKey();
            }); */

            modelBuilder.Entity<DatosEmpresa>(entity =>
            {
              //entity.HasNoKey();
              entity.HasKey(m=>m.DatosEmpresaId);
              entity.Property(e => e.DatosEmpresaId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ErrorSql>(entity =>
            {
                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            });

             modelBuilder.Entity<AbonosPrima>(entity =>
            {
              entity.HasKey(m => m.IdAbonoPrima);
              //entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            });

            /*modelBuilder.Entity<Importacion>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Importacion2>(entity =>
            {
                entity.HasNoKey();
            });*/

            modelBuilder.Entity<Lotes>(entity =>
            {
                entity.HasOne(d => d.IdBloqueNavigation)
                    .WithMany(p => p.Lotes)
                    .HasForeignKey(d => d.IdBloque)
                    .HasConstraintName("FK_Lotes_Bloques");
            });

            modelBuilder.Entity<Morosos>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Pagos>(entity =>
            {
                entity.HasOne(d => d.IdAsignacionNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdAsignacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagos_Asignaciones");
            });

            modelBuilder.Entity<Seguimientos>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IdSeguimiento).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ViewAsignacionesLotes>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Asignaciones_Lotes");
            });

            modelBuilder.Entity<ViewAsignacionesSaldo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Asignaciones_Saldo");
            });

            modelBuilder.Entity<ViewBloquesUbicacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Bloques_Ubicacion");
            });

            modelBuilder.Entity<ViewConsolidadoBloques>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Consolidado_Bloques");
            });

            modelBuilder.Entity<ViewConsolidadoUbicaciones>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Consolidado_Ubicaciones");
            });

            modelBuilder.Entity<ViewDashboard1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Dashboard_1");
            });

             modelBuilder.Entity<ViewDashBoard>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Dashboard");
            });

               modelBuilder.Entity<ViewAbonosPrima>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Abonos_Prima");
            });


            modelBuilder.Entity<ViewDepartamentosMunicipios>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Departamentos_Municipios");
            });

            modelBuilder.Entity<ViewGraficoPagos>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_GraficoPagos");

                entity.Property(e => e.Fecha).IsUnicode(false);
            });

            modelBuilder.Entity<ViewLotes>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Lotes");
            });

            modelBuilder.Entity<ViewPagosAsignaciones>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Pagos_Asignaciones");
            });

            modelBuilder.Entity<ViewReporteMorosos>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Reporte_Morosos");
            });

            modelBuilder.Entity<ViewReportePlanPago>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Reporte_PlanPago");
            });

            modelBuilder.Entity<ViewReporteTicketPago>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Reporte_TicketPago");
            });

            modelBuilder.Entity<ViewTotalAbonadoAsignaciones>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Total_Abonado_Asignaciones");
            });

            /* modelBuilder.Entity<ViewUsuariosSistema>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Usuarios_Sistema");
            }); */

            /*modelBuilder.Entity<VwAspnetApplications>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Applications");
            });

            modelBuilder.Entity<VwAspnetMembershipUsers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_MembershipUsers");
            });

            modelBuilder.Entity<VwAspnetProfiles>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Profiles");
            });

            modelBuilder.Entity<VwAspnetRoles>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Roles");
            });

            modelBuilder.Entity<VwAspnetUsers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_Users");
            });

            modelBuilder.Entity<VwAspnetUsersInRoles>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_UsersInRoles");
            });

            modelBuilder.Entity<VwAspnetWebPartStatePaths>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_WebPartState_Paths");
            });

            modelBuilder.Entity<VwAspnetWebPartStateShared>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_WebPartState_Shared");
            });

            modelBuilder.Entity<VwAspnetWebPartStateUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_aspnet_WebPartState_User");
            });*/

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
