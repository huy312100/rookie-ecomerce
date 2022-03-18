import React from 'react';

import useAsync from '../../hooks/useAsync';
import BrandServices from '../../services/BrandServices';

const BrandList = () => {
  const { data } = useAsync(BrandServices.getAllBrand); 
  return (
    <>
      {data.map((brand) => (
        <option key={brand.id} value={brand.id}>
          {brand.name}
        </option>
      ))}
    </>
  );
};

export default BrandList;