using System.Collections.Generic;
using Nop.Core.Domain.Directory;

namespace Nop.Services.Directory
{
    //JXzfl
    /// <summary>
    /// County service interface
    /// </summary>
    public partial interface ICountyService
    {
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="County">The state/province</param>
        void DeleteCounty(County county);

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="stateProvinceId">The state/province identifier</param>
        /// <returns>State/province</returns>
        County GetCountyById(int countyId);

        /// <summary>
        /// Gets a state/province 
        /// </summary>
        /// <param name="abbreviation">The state/province abbreviation</param>
        /// <returns>State/province</returns>
        County GetByAbbreviation(string abbreviation);
        
        /// <summary>
        /// Gets a state/province collection by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="languageId">Language identifier. It's used to sort states by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        IList<County> GetCountyByCityId(int cityId, bool showHidden = false);


        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="County">State/province</param>
        void InsertCounty(County County);

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="County">State/province</param>
        void UpdateCounty(County County);
    }
}
