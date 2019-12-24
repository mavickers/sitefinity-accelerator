namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Augmenting mapper interface - copy properties from one object to another
    /// </summary>
    /// <typeparam name="T1">Source type</typeparam>
    /// <typeparam name="T2">Target type</typeparam>
    public interface IAugmentingExternalMapper<in T1, T2>
    {
        /// <summary>
        /// Perform the augmenting map
        /// </summary>
        /// <param name="augmentingItem">The item containing the source properties</param>
        /// <param name="baseItem">The item receiving the source properties</param>
        /// <returns></returns>
        T2 Map(T1 augmentingItem, T2 baseItem);
    }
}
