using Microsoft.EntityFrameworkCore;

namespace ToMind.Data;

public sealed class ToMindDbContext : DbContext
{
    public ToMindDbContext(DbContextOptions<ToMindDbContext> options)
        : base(options)
    {
    }

    public DbSet<MindList> Lists => Set<MindList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<BoardCard> BoardCards => Set<BoardCard>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var list = modelBuilder.Entity<MindList>();
        list.ToTable("Lists");
        list.HasKey(l => l.Id);
        list.Property(l => l.Name).IsRequired();
        list.Property(l => l.Type).HasConversion<int>();
        list.Property(l => l.CreatedAtUtc).IsRequired();
        list.Property(l => l.UpdatedAtUtc).IsRequired();

        list.HasMany(l => l.TodoItems)
            .WithOne(i => i.List)
            .HasForeignKey(i => i.ListId);

        list.HasMany(l => l.BoardCards)
            .WithOne(c => c.List)
            .HasForeignKey(c => c.ListId);

        var todo = modelBuilder.Entity<TodoItem>();
        todo.ToTable("TodoItems");
        todo.HasKey(t => t.Id);
        todo.Property(t => t.Text).IsRequired();

        var card = modelBuilder.Entity<BoardCard>();
        card.ToTable("BoardCards");
        card.HasKey(c => c.Id);
        card.Property(c => c.Title).IsRequired();
        card.Property(c => c.Column).HasConversion<int>();
    }
}
