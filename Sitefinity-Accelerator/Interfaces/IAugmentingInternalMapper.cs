namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Augmenting mapper interface - copy properties onto a target object
    /// </summary>
    /// <remarks>
    /// The properties to be copied onto the target are to be generated/obtained from within the Map method.
    /// </remarks>
    /// <typeparam name="T">Target type</typeparam>
    public interface IAugmentingInternalMapper<T>
    {
        /// <summary>
        /// Perform the augmenting map
        /// </summary>
        /// <param name="item">The item receiving the properties</param>
        /// <returns></returns>
        T Map(T item);
    }
}
