using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class HotelContext : DbContext
    {
        // Контекст настроен для использования строки подключения "HotelContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "HotelDB.HotelContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "HotelContext" 
        // в файле конфигурации приложения.
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {

        }

        public DbSet<Status> RoomStatuses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Child> ClientsChilds { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> EmpPosition { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> RoomsCateroties { get; set; }

    }


    /// <summary>
    /// Сущность:бронирование
    /// </summary>
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int? StatusId { get; set; }
        public Status StatusRef { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string PersonNumber { get; set; }


    }
    /// <summary>
    /// Сущность:Клиент
    /// </summary>
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public string DocumentForm { get; set; }
        public int DocumentsSeries { get; set; }
        public int DocumentsNumber { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Child> Childs { get; set; }
        public Client()
        {
            this.Childs = new List<Child>();
        }

    }
    /// <summary>
    /// Сущность:Ребенок
    /// </summary>
    public class Child
    {
        [Key]
        public int ChildId { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }


    }
    /// <summary>
    /// Сущность:Состояние номера
    /// </summary>
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool Reservation { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public decimal Price { get; set; }
        public Reservation ReservationRef { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        
        public Status()
        {
            this.Clients = new List<Client>();
           
        }


    }
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int? PositionId { get; set; }
        public Position Position { get; set; }
        public string Adress { get; set; }
        public string Sex { get; set; }
        public string DocumentForm { get; set; }
        public int DocumentsSeries { get; set; }
        public int DocumentsNumber { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public Employee()
        {
            this.Statuses = new List<Status>();
        }



    }
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public Position()
        {
            this.Employees = new List<Employee>();
        }

    }
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Status> Statuses{get;set;}
        public Room()
        {
            this.Statuses = new List<Status>();
        }


    }
    public class Category
    {
        // первичный ключ
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int SeatsNumber { get; set; }
        public int RoomsNumber { get; set; }
        public decimal DelyPrice { get; set; }
        public string Inf { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public Category()
        {
            this.Rooms = new List<Room>();
        }

    }
}
