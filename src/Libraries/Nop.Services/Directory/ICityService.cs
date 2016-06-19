using System.Collections.Generic;
using Nop.Core.Domain.Directory;

namespace Nop.Services.Directory
{

    //JXzfl
    /// <summary>
    /// city service interface
    /// </summary>
    public partial interface ICityService
    {
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="City">The state/province</param>
        void DeleteCity(City city);

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="stateProvinceId">The state/province identifier</param>
        /// <returns>State/province</returns>
        City GetCityById(int cityId);

        /// <summary>
        /// Gets a state/province 
        /// </summary>
        /// <param name="abbreviation">The state/province abbreviation</param>
        /// <returns>State/province</returns>
        City GetByAbbreviation(string abbreviation);
        
        /// <summary>
        /// Gets a state/province collection by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="languageId">Language identifier. It's used to sort states by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        IList<City> GetCityByStateProvinceId(int stateProvince,  bool showHidden = false);


        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="City">State/province</param>
        void InsertCity(City City);

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="City">State/province</param>
        void UpdateCity(City City);
    }
}
