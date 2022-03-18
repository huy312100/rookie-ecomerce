import React from 'react';
// import { Link } from 'react-router-dom';
import {
  TableCell,
  TableBody,
  TableRow,
} from '@windmill/react-ui';

import MainModal from '../modal/MainModal';
import MainDrawer from '../drawer/MainDrawer';
import ProductDrawer from '../drawer/ProductDrawer';
import EditDeleteButton from '../table/EditDeleteButton';
import useToggleDrawer from '../../hooks/useToggleDrawer';

const ProductTable = ({ products }) => {
  const { serviceId, handleModalOpen, handleUpdate } = useToggleDrawer();
  // console.log(products);
  return (
    <>
      <MainModal id={serviceId} />
      <MainDrawer>
        <ProductDrawer id={serviceId} />
      </MainDrawer>

      <TableBody>
        {products.map((product, i) => (
          <TableRow key={i + 1}>
            <TableCell>
              <span className="text-xs uppercase font-semibold">
                {' '}
                {product.id}
              </span>
            </TableCell>
            <TableCell>
              <span className="text-sm font-medium">{product.name}</span>
            </TableCell>
            <TableCell>
              <span className="text-sm">{product.category.name}</span>
            </TableCell>

            <TableCell>
              <span className="text-sm font-semibold">{product.price}</span>
            </TableCell>

            <TableCell>              
              <span className="text-sm">{product.images.length > 0 ? <img src={product.images[0].imageUrl} alt='images'></img> : ""}</span>
            </TableCell>

            <TableCell>
              <span className="text-sm font-semibold">{product.createdDate}</span>
            </TableCell>

            <TableCell>
              <span className="text-sm font-semibold">{product.updatedDate}</span>
            </TableCell>

            
            <TableCell>
              <EditDeleteButton
                id={product.id}
                handleUpdate={handleUpdate}
                handleModalOpen={handleModalOpen}
              />
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </>
  );
};

export default React.memo(ProductTable);
