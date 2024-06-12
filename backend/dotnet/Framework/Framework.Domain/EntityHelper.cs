using System.Linq.Expressions;

namespace Framework.Domain
{
    /// <summary>
    /// Provides helper methods for working with entities.
    /// </summary>
    public class EntityHelper
    {
        /// <summary>
        /// Creates an equality expression for the entity's primary key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
        /// <param name="id">The value of the primary key to create the equality expression for.</param>
        /// <returns>An expression representing the equality comparison for the entity's primary key.</returns>
        /// <remarks>
        /// This method is useful for creating filter expressions based on the entity's primary key.
        /// </remarks>
        public static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId<TEntity, TKey>(TKey id)
            where TEntity : Entity<TKey>
        {
            // Create a parameter representing the entity in the expression tree.
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            // Create an equality comparison expression between the entity's Id property and the specified id value.
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, nameof(Entity<TKey>.Id)),
                Expression.Constant(id, typeof(TKey))
            );

            // Create a lambda expression representing the filter expression and return it.
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }

}
