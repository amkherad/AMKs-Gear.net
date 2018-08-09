using System;

namespace AMKsGear.Architecture.Patterns
{
    /// <summary>
    /// Represents a transaction.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Commits all changes to source.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks all changes.
        /// </summary>
        void Rollback();
    }
}