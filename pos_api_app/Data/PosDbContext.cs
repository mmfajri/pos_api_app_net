#nullable disable
using pos_api_app.Models.Entities;
using Microsoft.EntityFrameworkCore;
using pos_api_app.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace pos_api_app.Data;

public class PosDbContext : DbContext
{
	public PosDbContext(DbContextOptions<PosDbContext> options) : base(options) { }

	public DbSet<Account> Accounts { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Price> Prices { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<TransactionItem> TransactionItems { get; set; }
	public DbSet<Unit> Units { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
#nullable enable
			string? connectionString = GetConfig.AppSetting["ConnectionStrings:DefaultConnection"];

			if (string.IsNullOrEmpty(connectionString)) return;
			optionsBuilder.UseNpgsql(connectionString);
		}
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//Set Unique
		modelBuilder.Entity<Account>().HasIndex(emp => new
		{
			emp.UserName
		}).IsUnique();

		modelBuilder.Entity<Product>().HasIndex(x => new
		{
			x.BarcodeID
		}).IsUnique();

		modelBuilder.Entity<Unit>().HasIndex(x => new
		{
			x.Name
		}).IsUnique();

		//Set Relationship and Cardinality
		// Model
		modelBuilder.Entity<Account>()
		    .HasOne(Account => Account.Role)
		    .WithMany(Role => Role.Accounts)
		    .HasForeignKey(account => account.RoleId)
		    .OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<Account>()
		    .HasOne(account => account.Employee)
		    .WithOne(employee => employee.Account)
		    .HasForeignKey<Employee>(employee => employee.AccountId)
		    .OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<Account>()
			.HasMany(account => account.Transactions)
			.WithOne(transaction => transaction.Account)
			.HasForeignKey(transaction => transaction.AccountId)
			.OnDelete(DeleteBehavior.SetNull);

		// Employee
		modelBuilder.Entity<Employee>()
			.HasOne(employee => employee.Account)
			.WithOne(account => account.Employee)
			.HasForeignKey<Employee>(employee => employee.AccountId)
			.OnDelete(DeleteBehavior.SetNull);

		// Price
		modelBuilder.Entity<Price>()
			.HasOne(price => price.Product)
			.WithMany(product => product.Prices)
			.HasForeignKey(price => price.ProductId)
			.OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<Price>()
			.HasOne(price => price.Unit)
			.WithMany(unit => unit.Prices)
			.HasForeignKey(price => price.UnitId)
			.OnDelete(DeleteBehavior.SetNull);

		// Product
		modelBuilder.Entity<Product>()
		    .HasMany(product => product.Prices)
		    .WithOne(prices => prices.Product)
		    .HasForeignKey(prices => prices.ProductId)
		    .OnDelete(DeleteBehavior.SetNull);

		// Role
		modelBuilder.Entity<Role>()
			.HasMany(role => role.Accounts)
			.WithOne(account => account.Role)
			.HasForeignKey(account => account.RoleId)
			.OnDelete(DeleteBehavior.SetNull);

		// Transaction
		modelBuilder.Entity<Transaction>()
			.HasMany(transaction => transaction.TransactionItems)
			.WithOne(transactionItem => transactionItem.Transaction)
			.HasForeignKey(transactionItem => transactionItem.TransactionId)
			.OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<Transaction>()
			.HasOne(transaction => transaction.Account)
			.WithMany(account => account.Transactions)
			.HasForeignKey(transaction => transaction.AccountId)
			.OnDelete(DeleteBehavior.SetNull);

		// Transaction Item
		modelBuilder.Entity<TransactionItem>()
		    .HasOne(TransactionItem => TransactionItem.Transaction)
		    .WithMany(transaction => transaction.TransactionItems)
		    .HasForeignKey(transactions_item => transactions_item.TransactionId)
		    .OnDelete(DeleteBehavior.SetNull);

		//Unit 
		modelBuilder.Entity<Unit>()
		    .HasMany(unit => unit.Prices)
		    .WithOne(prices => prices.Unit)
		    .HasForeignKey(prices => prices.UnitId)
		    .OnDelete(DeleteBehavior.SetNull);

	}

}
