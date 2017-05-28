using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HotelProject.Models;

namespace HotelProject.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20170520083608_newinit")]
    partial class newinit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelProject.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("DelyPrice");

                    b.Property<string>("Inf");

                    b.Property<string>("Name");

                    b.Property<int>("RoomsNumber");

                    b.Property<int>("SeatsNumber");

                    b.HasKey("CategoryId");

                    b.ToTable("RoomsCateroties");
                });

            modelBuilder.Entity("HotelProject.Models.Child", b =>
                {
                    b.Property<int>("ChildId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<int?>("ClientId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ChildId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientsChilds");
                });

            modelBuilder.Entity("HotelProject.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("DocumentForm");

                    b.Property<int>("DocumentsNumber");

                    b.Property<int>("DocumentsSeries");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.Property<string>("Sex");

                    b.Property<int?>("StatusId");

                    b.HasKey("ClientId");

                    b.HasIndex("StatusId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("HotelProject.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("DocumentForm");

                    b.Property<int>("DocumentsNumber");

                    b.Property<int>("DocumentsSeries");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.Property<int?>("PositionId");

                    b.Property<string>("Sex");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HotelProject.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("PositionId");

                    b.ToTable("EmpPosition");
                });

            modelBuilder.Entity("HotelProject.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientName");

                    b.Property<string>("PersonNumber");

                    b.Property<string>("Phone");

                    b.Property<int?>("StatusId");

                    b.HasKey("ReservationId");

                    b.HasIndex("StatusId")
                        .IsUnique();

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelProject.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.HasKey("RoomId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelProject.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Arrival");

                    b.Property<DateTime>("Departure");

                    b.Property<int?>("EmployeeId");

                    b.Property<decimal>("Price");

                    b.Property<bool>("Reservation");

                    b.Property<int?>("RoomId");

                    b.HasKey("StatusId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomStatuses");
                });

            modelBuilder.Entity("HotelProject.Models.Child", b =>
                {
                    b.HasOne("HotelProject.Models.Client", "Client")
                        .WithMany("Childs")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("HotelProject.Models.Client", b =>
                {
                    b.HasOne("HotelProject.Models.Status", "Status")
                        .WithMany("Clients")
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("HotelProject.Models.Employee", b =>
                {
                    b.HasOne("HotelProject.Models.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("HotelProject.Models.Reservation", b =>
                {
                    b.HasOne("HotelProject.Models.Status", "StatusRef")
                        .WithOne("ReservationRef")
                        .HasForeignKey("HotelProject.Models.Reservation", "StatusId");
                });

            modelBuilder.Entity("HotelProject.Models.Room", b =>
                {
                    b.HasOne("HotelProject.Models.Category", "Category")
                        .WithMany("Rooms")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("HotelProject.Models.Status", b =>
                {
                    b.HasOne("HotelProject.Models.Employee", "Employee")
                        .WithMany("Statuses")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HotelProject.Models.Room", "Room")
                        .WithMany("Statuses")
                        .HasForeignKey("RoomId");
                });
        }
    }
}
