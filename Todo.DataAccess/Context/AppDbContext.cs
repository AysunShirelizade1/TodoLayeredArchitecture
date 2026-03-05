using Microsoft.EntityFrameworkCore;
using Todo.Entities.Models;

namespace Todo.DataAccess.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoStatus> TodoStatuses => Set<TodoStatus>();
    public DbSet<TodoPriority> TodoPriorities => Set<TodoPriority>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TodoItemTag> TodoItemTags => Set<TodoItemTag>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var seedCreatedAt = new DateTime(2026, 3, 5, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.ToTable("TodoItems");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .HasMaxLength(2000);

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            // Status (1) -> (N) TodoItems
            entity.HasOne(x => x.Status)
                .WithMany(s => s.TodoItems)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Priority (1) -> (N) TodoItems
            entity.HasOne(x => x.Priority)
                .WithMany(p => p.TodoItems)
                .HasForeignKey(x => x.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Faydalı index-lər
            entity.HasIndex(x => x.CreatedAt);
            entity.HasIndex(x => x.DueDate);
            entity.HasIndex(x => x.StatusId);
            entity.HasIndex(x => x.PriorityId);
        });

        // ----------------------------
        // TodoStatus (lookup)
        // ----------------------------
        modelBuilder.Entity<TodoStatus>(entity =>
        {
            entity.ToTable("TodoStatuses");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(x => x.Name).IsUnique();

            entity.HasData(
                new TodoStatus { Id = 1, Name = "New",        CreatedAt = seedCreatedAt },
                new TodoStatus { Id = 2, Name = "InProgress", CreatedAt = seedCreatedAt },
                new TodoStatus { Id = 3, Name = "Done",       CreatedAt = seedCreatedAt },
                new TodoStatus { Id = 4, Name = "Archived",   CreatedAt = seedCreatedAt }
            );
        });

        // ----------------------------
        // TodoPriority (lookup)
        // ----------------------------
        modelBuilder.Entity<TodoPriority>(entity =>
        {
            entity.ToTable("TodoPriorities");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.Level)
                .IsRequired();

            entity.HasIndex(x => x.Name).IsUnique();
            entity.HasIndex(x => x.Level).IsUnique();

            entity.HasData(
                new TodoPriority { Id = 1, Name = "Low",      Level = 1, CreatedAt = seedCreatedAt },
                new TodoPriority { Id = 2, Name = "Medium",   Level = 2, CreatedAt = seedCreatedAt },
                new TodoPriority { Id = 3, Name = "High",     Level = 3, CreatedAt = seedCreatedAt },
                new TodoPriority { Id = 4, Name = "Critical", Level = 4, CreatedAt = seedCreatedAt }
            );
        });

        // ----------------------------
        // Tag
        // ----------------------------
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tags");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(60);

            entity.HasIndex(x => x.Name).IsUnique();
        });

        // ----------------------------
        // Comment
        // ----------------------------
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comments");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(2000);

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            // TodoItem (1) -> (N) Comments
            entity.HasOne(x => x.TodoItem)
                .WithMany(t => t.Comments)
                .HasForeignKey(x => x.TodoItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => x.TodoItemId);
            entity.HasIndex(x => x.CreatedAt);
        });

        // ----------------------------
        // TodoItemTag (Many-to-Many join)
        // ----------------------------
        modelBuilder.Entity<TodoItemTag>(entity =>
        {
            entity.ToTable("TodoItemTags");

            // Composite PK
            entity.HasKey(x => new { x.TodoItemId, x.TagId });

            entity.HasOne(x => x.TodoItem)
                .WithMany(t => t.TodoItemTags)
                .HasForeignKey(x => x.TodoItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Tag)
                .WithMany(t => t.TodoItemTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => x.TagId);
        });
    }
}