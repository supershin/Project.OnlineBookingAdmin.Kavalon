using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project.Booking.Admin.Data.Models
{
    public partial class OnlineBookingDbContext : DbContext
    {
        public OnlineBookingDbContext()
        {
        }

        public OnlineBookingDbContext(DbContextOptions<OnlineBookingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<temp_agent> temp_agent { get; set; }
        public virtual DbSet<temp_agent_30> temp_agent_30 { get; set; }
        public virtual DbSet<temp_agent_31> temp_agent_31 { get; set; }
        public virtual DbSet<temp_agent_heritage> temp_agent_heritage { get; set; }
        public virtual DbSet<temp_allow_book> temp_allow_book { get; set; }
        public virtual DbSet<temp_annotation> temp_annotation { get; set; }
        public virtual DbSet<temp_minburi> temp_minburi { get; set; }
        public virtual DbSet<temp_model_type_resource> temp_model_type_resource { get; set; }
        public virtual DbSet<temp_pricelist> temp_pricelist { get; set; }
        public virtual DbSet<temp_quota_30> temp_quota_30 { get; set; }
        public virtual DbSet<temp_quota_31> temp_quota_31 { get; set; }
        public virtual DbSet<temp_resource_project_gallery> temp_resource_project_gallery { get; set; }
        public virtual DbSet<temp_reward> temp_reward { get; set; }
        public virtual DbSet<temp_unit> temp_unit { get; set; }
        public virtual DbSet<temp_unit_email> temp_unit_email { get; set; }
        public virtual DbSet<temp_unit_vip> temp_unit_vip { get; set; }
        public virtual DbSet<tm_Annotation> tm_Annotation { get; set; }
        public virtual DbSet<tm_BU> tm_BU { get; set; }
        public virtual DbSet<tm_BookingStatus> tm_BookingStatus { get; set; }
        public virtual DbSet<tm_Build> tm_Build { get; set; }
        public virtual DbSet<tm_Company> tm_Company { get; set; }
        public virtual DbSet<tm_Department> tm_Department { get; set; }
        public virtual DbSet<tm_Ext> tm_Ext { get; set; }
        public virtual DbSet<tm_ExtType> tm_ExtType { get; set; }
        public virtual DbSet<tm_Floor> tm_Floor { get; set; }
        public virtual DbSet<tm_ModelType> tm_ModelType { get; set; }
        public virtual DbSet<tm_OmiseChargeStatus> tm_OmiseChargeStatus { get; set; }
        public virtual DbSet<tm_OrderStatus> tm_OrderStatus { get; set; }
        public virtual DbSet<tm_Project> tm_Project { get; set; }
        public virtual DbSet<tm_ProjectConfig> tm_ProjectConfig { get; set; }
        public virtual DbSet<tm_Register> tm_Register { get; set; }
        public virtual DbSet<tm_Resource> tm_Resource { get; set; }
        public virtual DbSet<tm_Role> tm_Role { get; set; }
        public virtual DbSet<tm_Unit> tm_Unit { get; set; }
        public virtual DbSet<tm_UnitStatus> tm_UnitStatus { get; set; }
        public virtual DbSet<tm_UnitType> tm_UnitType { get; set; }
        public virtual DbSet<tm_User> tm_User { get; set; }
        public virtual DbSet<tm_WebImage> tm_WebImage { get; set; }
        public virtual DbSet<tr_DepartmentBU_Mapping> tr_DepartmentBU_Mapping { get; set; }
        public virtual DbSet<tr_Matrix_Config> tr_Matrix_Config { get; set; }
        public virtual DbSet<tr_ProjectBuild> tr_ProjectBuild { get; set; }
        public virtual DbSet<tr_ProjectBuildFloor> tr_ProjectBuildFloor { get; set; }
        public virtual DbSet<tr_ProjectBuildGroup> tr_ProjectBuildGroup { get; set; }
        public virtual DbSet<tr_ProjectGallery> tr_ProjectGallery { get; set; }
        public virtual DbSet<tr_ProjectGalleryResource> tr_ProjectGalleryResource { get; set; }
        public virtual DbSet<tr_ProjectModelType> tr_ProjectModelType { get; set; }
        public virtual DbSet<tr_ProjectQuota> tr_ProjectQuota { get; set; }
        public virtual DbSet<tr_ProjectRegisterQuota> tr_ProjectRegisterQuota { get; set; }
        public virtual DbSet<tr_ProjectResource> tr_ProjectResource { get; set; }
        public virtual DbSet<tr_ProjectSpecialPromotion> tr_ProjectSpecialPromotion { get; set; }
        public virtual DbSet<tr_ProjectTransferPayment> tr_ProjectTransferPayment { get; set; }
        public virtual DbSet<tr_UnitReserve> tr_UnitReserve { get; set; }
        public virtual DbSet<tr_UnitSpecialPromotion> tr_UnitSpecialPromotion { get; set; }
        public virtual DbSet<tr_UnitVIP> tr_UnitVIP { get; set; }
        public virtual DbSet<ts_Booking> ts_Booking { get; set; }
        public virtual DbSet<ts_Booking_20231113> ts_Booking_20231113 { get; set; }
        public virtual DbSet<ts_Booking_VIP> ts_Booking_VIP { get; set; }
        public virtual DbSet<ts_Order> ts_Order { get; set; }
        public virtual DbSet<ts_OrderDetail> ts_OrderDetail { get; set; }
        public virtual DbSet<ts_Payment> ts_Payment { get; set; }
        public virtual DbSet<ts_Payment_Credit> ts_Payment_Credit { get; set; }
        public virtual DbSet<ts_Payment_Transfer> ts_Payment_Transfer { get; set; }
        public virtual DbSet<ts_Unitbooking_History> ts_Unitbooking_History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<temp_allow_book>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Agency).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<temp_annotation>(entity =>
            {
                entity.Property(e => e.SelectorType).IsUnicode(false);
            });

            modelBuilder.Entity<temp_pricelist>(entity =>
            {
                entity.Property(e => e.UnitCode).IsUnicode(false);

                entity.Property(e => e.ModelType).IsUnicode(false);

                entity.Property(e => e.UnitType).IsUnicode(false);
            });

            modelBuilder.Entity<temp_quota_30>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);
            });

            modelBuilder.Entity<temp_quota_31>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<temp_reward>(entity =>
            {
                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<temp_unit_email>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.UnitCode).IsUnicode(false);
            });

            modelBuilder.Entity<temp_unit_vip>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.UnitCode).IsUnicode(false);
            });

            modelBuilder.Entity<tm_Annotation>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.SelectorType).IsUnicode(false);

                entity.Property(e => e.SelectorValue).IsUnicode(false);
            });

            modelBuilder.Entity<tm_BU>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<tm_BookingStatus>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Color).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Build>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Company>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CompanyID).IsUnicode(false);

                entity.Property(e => e.OmisePublicKey).IsUnicode(false);

                entity.Property(e => e.OmiseSecurityKey).IsUnicode(false);

                entity.Property(e => e.TransferAccountNo).IsUnicode(false);

                entity.Property(e => e.TransferBank).IsUnicode(false);
            });

            modelBuilder.Entity<tm_Department>(entity =>
            {
                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Ext>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ExtType)
                    .WithMany(p => p.tm_Ext)
                    .HasForeignKey(d => d.ExtTypeID)
                    .HasConstraintName("FK_tm_Ext_tm_ExtType");
            });

            modelBuilder.Entity<tm_ExtType>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Floor>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_ModelType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDtae).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_OmiseChargeStatus>(entity =>
            {
                entity.Property(e => e.ID).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<tm_OrderStatus>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Color).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Project>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProjectCode).IsUnicode(false);

                entity.Property(e => e.ProjectNameEN).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BU)
                    .WithMany(p => p.tm_Project)
                    .HasForeignKey(d => d.BUID)
                    .HasConstraintName("FK_tm_Project_tm_BU");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.tm_Project)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_tm_Project_tm_Company");
            });

            modelBuilder.Entity<tm_ProjectConfig>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tm_ProjectConfig)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tm_ProjectConfig_tm_Project");
            });

            modelBuilder.Entity<tm_Register>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CitizenID).IsUnicode(false);

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.RegisterType)
                    .WithMany(p => p.tm_Register)
                    .HasForeignKey(d => d.RegisterTypeID)
                    .HasConstraintName("FK_tm_Register_tm_Ext");
            });

            modelBuilder.Entity<tm_Resource>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MimeType).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Role>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_Unit>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitCode).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserUpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Annotation)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.AnnotationID)
                    .HasConstraintName("FK_tm_Unit_tm_Annotation");

                entity.HasOne(d => d.Build)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.BuildID)
                    .HasConstraintName("FK_tm_Unit_tm_Build");

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.FloorID)
                    .HasConstraintName("FK_tm_Unit_tm_Floor");

                entity.HasOne(d => d.ModelType)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.ModelTypeID)
                    .HasConstraintName("FK_tm_Unit_tm_ModelType");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tm_Unit_tm_Project");

                entity.HasOne(d => d.UnitStatus)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.UnitStatusID)
                    .HasConstraintName("FK_tm_Unit_tm_UnitStatus");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.UnitTypeID)
                    .HasConstraintName("FK_tm_Unit_tm_UnitType");

                entity.HasOne(d => d.UserUpdateBy)
                    .WithMany(p => p.tm_Unit)
                    .HasForeignKey(d => d.UserUpdateByID)
                    .HasConstraintName("FK_tm_Unit_tm_User");
            });

            modelBuilder.Entity<tm_UnitStatus>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Color).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_UnitType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tm_User>(entity =>
            {
                entity.Property(e => e.ID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.tm_User)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_tm_User_tm_Ext");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.tm_User)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_tm_User_tm_Role");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.InverseUpdateByNavigation)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("UserUpdateBy");
            });

            modelBuilder.Entity<tm_WebImage>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.tm_WebImage)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_tm_WebImage_tm_Resource");
            });

            modelBuilder.Entity<tr_DepartmentBU_Mapping>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.HasOne(d => d.BU)
                    .WithMany(p => p.tr_DepartmentBU_Mapping)
                    .HasForeignKey(d => d.BUID)
                    .HasConstraintName("FK_tr_DepartmentBU_Mapping_tm_BU");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.tr_DepartmentBU_Mapping)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_tr_DepartmentBU_Mapping_tm_Department");
            });

            modelBuilder.Entity<tr_Matrix_Config>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Style).IsUnicode(false);

                entity.HasOne(d => d.Build)
                    .WithMany(p => p.tr_Matrix_Config)
                    .HasForeignKey(d => d.BuildID)
                    .HasConstraintName("FK_tr_Matrix_Config_tm_Build");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_Matrix_Config)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_Matrix_Config_tm_Project");
            });

            modelBuilder.Entity<tr_ProjectBuild>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Build)
                    .WithMany(p => p.tr_ProjectBuild)
                    .HasForeignKey(d => d.BuildID)
                    .HasConstraintName("FK_tr_ProjectBuild_tm_Build");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectBuild)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectBuild_tm_Project");
            });

            modelBuilder.Entity<tr_ProjectBuildFloor>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Build)
                    .WithMany(p => p.tr_ProjectBuildFloor)
                    .HasForeignKey(d => d.BuildID)
                    .HasConstraintName("FK_tr_ProjectBuildFloor_tm_Build");

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.tr_ProjectBuildFloor)
                    .HasForeignKey(d => d.FloorID)
                    .HasConstraintName("FK_tr_ProjectBuildFloor_tm_Floor");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectBuildFloor)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectBuildFloor_tm_Project");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.tr_ProjectBuildFloor)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_tr_ProjectBuildFloor_tm_Resource");
            });

            modelBuilder.Entity<tr_ProjectBuildGroup>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.HasOne(d => d.Build)
                    .WithMany(p => p.tr_ProjectBuildGroupBuild)
                    .HasForeignKey(d => d.BuildID)
                    .HasConstraintName("FK_tr_ProjectBuildGroup_tm_Build");

                entity.HasOne(d => d.ParentBuild)
                    .WithMany(p => p.tr_ProjectBuildGroupParentBuild)
                    .HasForeignKey(d => d.ParentBuildID)
                    .HasConstraintName("FK_tr_ProjectBuildGroup_tm_Build1");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectBuildGroup)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectBuildGroup_tm_Project");
            });

            modelBuilder.Entity<tr_ProjectGallery>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActie).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectGallery)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectGallery_tm_Project");
            });

            modelBuilder.Entity<tr_ProjectGalleryResource>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActie).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ProjectGallery)
                    .WithMany(p => p.tr_ProjectGalleryResource)
                    .HasForeignKey(d => d.ProjectGalleryID)
                    .HasConstraintName("FK_tr_ProjectGalleryResource_tr_ProjectGallery");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.tr_ProjectGalleryResource)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_tr_ProjectGalleryResource_tm_Resource");
            });

            modelBuilder.Entity<tr_ProjectModelType>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ModelType)
                    .WithMany(p => p.tr_ProjectModelType)
                    .HasForeignKey(d => d.ModelTypeID)
                    .HasConstraintName("FK_tr_ProjectModelType_tm_ModelType");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectModelType)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectModelType_tm_Project");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.tr_ProjectModelType)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_tr_ProjectModelType_tm_Resource");
            });

            modelBuilder.Entity<tr_ProjectQuota>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<tr_ProjectRegisterQuota>(entity =>
            {
                entity.Property(e => e.CancelRemark).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectRegisterQuota)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectRegisterQuota_tm_Project");

                entity.HasOne(d => d.ProjectQuota)
                    .WithMany(p => p.tr_ProjectRegisterQuota)
                    .HasForeignKey(d => d.ProjectQuotaID)
                    .HasConstraintName("FK_tr_ProjectRegisterQuota_tr_ProjectQuota");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.tr_ProjectRegisterQuota)
                    .HasForeignKey(d => d.RegisterID)
                    .HasConstraintName("FK_tr_ProjectRegisterQuota_tm_Register");
            });

            modelBuilder.Entity<tr_ProjectResource>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectResource)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectResource_tm_Project");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.tr_ProjectResource)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_tr_ProjectResource_tm_Resource");

                entity.HasOne(d => d.ResourceType)
                    .WithMany(p => p.tr_ProjectResource)
                    .HasForeignKey(d => d.ResourceTypeID)
                    .HasConstraintName("FK_tr_ProjectResource_tm_Ext");
            });

            modelBuilder.Entity<tr_ProjectSpecialPromotion>(entity =>
            {
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectSpecialPromotion)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectSpecialPromotion_tm_Project");
            });

            modelBuilder.Entity<tr_ProjectTransferPayment>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CraeteDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.tr_ProjectTransferPayment)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_tr_ProjectTransferPayment_tm_Project");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.tr_ProjectTransferPayment)
                    .HasForeignKey(d => d.RegisterID)
                    .HasConstraintName("FK_tr_ProjectTransferPayment_tm_Register");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.tr_ProjectTransferPayment)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_tr_ProjectTransferPayment_tm_Resource");
            });

            modelBuilder.Entity<tr_UnitReserve>(entity =>
            {
                entity.Property(e => e.UnitID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReserverUserEmail).IsUnicode(false);

                entity.HasOne(d => d.Unit)
                    .WithOne(p => p.tr_UnitReserve)
                    .HasForeignKey<tr_UnitReserve>(d => d.UnitID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tr_UnitReserve_tm_Unit");
            });

            modelBuilder.Entity<tr_UnitSpecialPromotion>(entity =>
            {
                entity.HasOne(d => d.SpecialPromotion)
                    .WithMany(p => p.tr_UnitSpecialPromotion)
                    .HasForeignKey(d => d.SpecialPromotionID)
                    .HasConstraintName("FK_tr_UnitSpecialPromotion_tr_ProjectSpecialPromotion");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.tr_UnitSpecialPromotion)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_tr_UnitSpecialPromotion_tm_Unit");
            });

            modelBuilder.Entity<tr_UnitVIP>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.tr_UnitVIP)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_tr_UnitVIP_tm_Unit");
            });

            modelBuilder.Entity<ts_Booking>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.BookingNumber).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerCitizenID).IsUnicode(false);

                entity.Property(e => e.CustomerEmail).IsUnicode(false);

                entity.Property(e => e.CustomerMobile).IsUnicode(false);

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserUpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.ts_Booking)
                    .HasForeignKey(d => d.BookingStatusID)
                    .HasConstraintName("FK_ts_Booking_tm_BookingStatus");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ts_Booking)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_ts_Booking_tm_Register");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ts_Booking)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_ts_Booking_tm_Project");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ts_Booking)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_ts_Booking_tm_Unit");

                entity.HasOne(d => d.UserUpdateBy)
                    .WithMany(p => p.ts_Booking)
                    .HasForeignKey(d => d.UserUpdateByID)
                    .HasConstraintName("FK_ts_Booking_tm_User");
            });

            modelBuilder.Entity<ts_Booking_20231113>(entity =>
            {
                entity.Property(e => e.BookingNumber).IsUnicode(false);

                entity.Property(e => e.CustomerCitizenID).IsUnicode(false);

                entity.Property(e => e.CustomerEmail).IsUnicode(false);

                entity.Property(e => e.CustomerMobile).IsUnicode(false);
            });

            modelBuilder.Entity<ts_Booking_VIP>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.BookingNumber).IsUnicode(false);

                entity.Property(e => e.CustomerCitizen).IsUnicode(false);

                entity.Property(e => e.CustomerEmail).IsUnicode(false);

                entity.Property(e => e.CustomerMobile).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.UnitCode).IsUnicode(false);
            });

            modelBuilder.Entity<ts_Order>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerEmail).IsUnicode(false);

                entity.Property(e => e.CustomerMobile).IsUnicode(false);

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.ts_Order)
                    .HasForeignKey(d => d.OrderStatusID)
                    .HasConstraintName("FK_ts_Order_tm_OrderStatus");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ts_Order)
                    .HasForeignKey(d => d.ProjectID)
                    .HasConstraintName("FK_ts_Order_tm_Project");
            });

            modelBuilder.Entity<ts_OrderDetail>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ts_Payment>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentNo).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.ts_Payment)
                    .HasForeignKey(d => d.BookingID)
                    .HasConstraintName("FK_ts_Payment_ts_Booking");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.ts_Payment)
                    .HasForeignKey(d => d.PaymentTypeID)
                    .HasConstraintName("FK_ts_Payment_tm_Ext");

                entity.HasOne(d => d.ProjectRegisterQuota)
                    .WithMany(p => p.ts_Payment)
                    .HasForeignKey(d => d.ProjectRegisterQuotaID)
                    .HasConstraintName("FK_ts_Payment_tr_ProjectRegisterQuota");
            });

            modelBuilder.Entity<ts_Payment_Credit>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.AuthorizeURI).IsUnicode(false);

                entity.Property(e => e.CardBank).IsUnicode(false);

                entity.Property(e => e.CardBrand).IsUnicode(false);

                entity.Property(e => e.CardFinancing).IsUnicode(false);

                entity.Property(e => e.CardFirstDigit).IsUnicode(false);

                entity.Property(e => e.CardLastDigit).IsUnicode(false);

                entity.Property(e => e.CardName).IsUnicode(false);

                entity.Property(e => e.ChargeID).IsUnicode(false);

                entity.Property(e => e.Currency).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FailureCode).IsUnicode(false);

                entity.Property(e => e.IP).IsUnicode(false);

                entity.Property(e => e.ReturnURI).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.ts_Payment_Credit)
                    .HasForeignKey(d => d.PaymentID)
                    .HasConstraintName("FK_ts_Payment_Credit_ts_Payment");
            });

            modelBuilder.Entity<ts_Payment_Transfer>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Hours).IsUnicode(false);

                entity.Property(e => e.Minutes).IsUnicode(false);

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ts_Payment_Transfer)
                    .HasForeignKey(d => d.ResourceID)
                    .HasConstraintName("FK_ts_Payment_Transfer_tm_Resource");
            });

            modelBuilder.Entity<ts_Unitbooking_History>(entity =>
            {
                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.ts_Unitbooking_History)
                    .HasForeignKey(d => d.BookingID)
                    .HasConstraintName("FK_ts_Unitbooking_History_ts_Booking");

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.ts_Unitbooking_History)
                    .HasForeignKey(d => d.BookingStatusID)
                    .HasConstraintName("FK_ts_Unitbooking_History_tm_BookingStatus");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ts_Unitbooking_History)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_ts_Unitbooking_History_tm_Unit");

                entity.HasOne(d => d.UnitStatus)
                    .WithMany(p => p.ts_Unitbooking_History)
                    .HasForeignKey(d => d.UnitStatusID)
                    .HasConstraintName("FK_ts_Unitbooking_History_tm_UnitStatus");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
