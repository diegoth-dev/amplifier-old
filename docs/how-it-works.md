# How it works

To make the process of creating SaaS applications easy, Amplifier provides interfaces that your classes must implement to enable features such as multi-tenant support, auditing, and soft delete automatically.

## Interfaces

### Multi-tenant interfaces

`IHaveTenant` - Implement this interface for an entity which must have a non-nullable TenantId.

`IMayHaveTenant` - Implement this interface for an entity which must have a nullable TenantId.

`ITenantFilter` - Implement this interface for an entity that must be globally filtered by Tenant Id.

`IUserSession` - Interface that represents a user session.

### Auditing interfaces

`IFullAuditedEntity` - Implement this interface for an entity that must be fully audited (Creation, deletion, updating times and users).

`ISoftDelete` - Implement this interface for an entity that must be soft deleted.

### UserSession middleware

After authenticated by your application, UserSession middleware will set the properties from the values in the ClaimsPrincipal found in context.User in your UserSession class.

## Entity Framework Core implementation

### Shadow Properties

Amplifier Entity Framework Core implementation uses shadow properties to add properties to entities that implement any of the interfaces listed above.

!!! info "Shadow Properties"
    Shadow properties are properties that are not defined in your .NET entity class but are defined for that entity type in the EF Core model. The value and state of these properties are maintained purely in the Change Tracker.

    Shadow properties are useful when there is data in the database that should not be exposed on the mapped entity types.

    To learn more about shadow properties, visit the [Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties).

`IHaveTenant` interface adds a TenantId shadow property.

`IMayHaveTenant` interface adds a nullable TenantId shadow property.

`IFullAuditedEntity` interface adds a CreationTime, CreationUser, LastModificationTime, LastModificationUser, DeletionTime, DeletionUser shadow properties.

`ISoftDelete` adds an IsDeleted shadow property.

### Global Query Filters

Using global query filters, Amplifier can filter your entities based on their shadow properties. To be filtered by TenantId, an entity needs to implement ITenantFilter interface. Models can have an ownership hierarchy with many levels deep and filtering by TenantId on each level can degrade performance. Only root classes need to implement this interface since their children are safely joined to their parents via other properties.

!!! info "Global Query Filters"
    Global query filters are LINQ query predicates (a boolean expression typically passed to the LINQ Where query operator) applied to Entity Types in the metadata model (usually in OnModelCreating). Such filters are automatically applied to any LINQ queries involving those Entity Types, including Entity Types referenced indirectly, such as through the use of Include or direct navigation property references. 

    To learn more about global query filters, visit the [Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/querying/filters).
