# Model
Use this class project to define any model classes

## Reuse for the project
### 1. Filter deleted recods via configuration
```
modelBuilder.Entity<YourEntity>()
    .HasQueryFilter(e => !e.IsDeleted);
```

Note: Change <YourEntity> to specific model class while defining the IEntityTypeConfiguration<>