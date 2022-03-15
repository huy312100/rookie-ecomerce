import React from 'react';
import { TableBody, TableRow, TableCell } from '@windmill/react-ui';

import MainModal from '../modal/MainModal';
import MainDrawer from '../drawer/MainDrawer';
import CategoryDrawer from '../drawer/CategoryDrawer';
// import useToggleDrawer from '../../hooks/useToggleDrawer';
import EditDeleteButton from '../table/EditDeleteButton';

const CategoryTable = ({ categories }) => {
  // const { serviceId, handleModalOpen, handleUpdate } = useToggleDrawer();
  return (
    <>
      <MainModal  />
      <MainDrawer>
        <CategoryDrawer  />
      </MainDrawer>

      <TableBody>
        {categories?.map((data) => (
          <TableRow key={data.id}>
            <TableCell className="font-semibold uppercase text-xs">
              {data.id}
            </TableCell>

            <TableCell className="font-medium text-sm">
              {data.name}
            </TableCell>
            <TableCell className="text-sm ">{data.description}</TableCell>
            <TableCell>
              <EditDeleteButton
                id={data.id}
                // handleUpdate={handleUpdate}
                // handleModalOpen={handleModalOpen}
              />
            </TableCell>
           
          </TableRow>
        ))}
      </TableBody>
    </>
  );
};

export default CategoryTable;
