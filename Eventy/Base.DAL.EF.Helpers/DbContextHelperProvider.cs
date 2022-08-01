using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Base.DAL.EF.Helpers;


/// <summary>
/// Database Layer Helpers.
/// </summary>
public static class DbContextHelperProvider
{
    
    /// <summary>
    /// Method Removes The Cascade Delete For Every Entity Relationship.
    /// </summary>
    /// <param name="builder">Provides a Simple API Surface For Configuring a Model That Defines The Shape Entities</param>
    public static void RemoveCascadeDelete(ModelBuilder builder)
    {
        // Remove The Cascade Delete For Every Entity Relationship
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .Where(x => !x.IsOwned())
                     .SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    
    /// <summary>
    /// Method Handles Metadata Insertion and Change In Database.
    /// </summary>
    /// <param name="changeTracker">Database Change Tracking Information Connection.</param>
    public static void SaveChangesMetadataUpdate(ChangeTracker changeTracker)
    {
        
        // Update The State of EF Tracked Objects
        changeTracker.DetectChanges();

        // Get Modified Entities That Were Added And Change Their Metadata State.
        var markedAsAddedEntities = changeTracker.Entries().Where(x => x.State == EntityState.Added);
        
        foreach (var entityEntry in markedAsAddedEntities)
        {
            // Check If Implements Needed Interface. (Stores Actually Metadata Fields)
            if (entityEntry.Entity is not IDomainEntityMeta entity) continue;

            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = "System";
            
            // Do Not Let Insertion Of Modified Meta Data Into Database.
            entity.ModifiedAt = null;
            entity.ModifiedBy = null;
        }

        // Get Modified Entities That Were Updated And Change Their Metadata State.
        var markedAsModifiedEntities = changeTracker.Entries().Where(x => x.State == EntityState.Modified);
        
        foreach (var entityEntry in markedAsModifiedEntities)
        {
            // Check If Implements Needed Interface. (Stores Actually Metadata Fields)
            if (entityEntry.Entity is not IDomainEntityMeta entity) continue;

            entity.ModifiedAt = DateTime.UtcNow;
            entity.ModifiedBy = "System";

            // Do Not Let Insertion Of Creation Meta Data Into Database. (Keep Old Data)
            entityEntry.Property(nameof(entity.CreatedAt)).IsModified = false;
            entityEntry.Property(nameof(entity.CreatedBy)).IsModified = false;
        }
    }
}