import React, { useContext, useState } from 'react';
import { Link } from 'react-router-dom';
import * as dayjs from 'dayjs';
import { TableCell, TableBody, TableRow } from '@windmill/react-ui';

import MainModal from '../modal/MainModal';
import { SidebarContext } from '../../context/SidebarContext';

const CustomerTable = ({ customers }) => {
  const [customerId, setCustomerId] = useState('');
  const { toggleModal } = useContext(SidebarContext);

  const handleModalOpen = (id) => {
    setCustomerId(id);
    toggleModal();
  };

  return (
    <>
      <MainModal id={customerId} />
      <TableBody>
        {customers?.map((user) => (
          <TableRow key={user._id}>
            <TableCell>
              <span className="font-semibold uppercase text-xs">
                {' '}
                {user.id}
              </span>
            </TableCell>
            <TableCell>
              <span className="text-sm">{user.username}</span>
            </TableCell>
            <TableCell>
              <span className="text-sm">{user.firstName}</span>{' '}
            </TableCell>
            <TableCell>
              <span className="text-sm font-medium">{user.lastName}</span>
            </TableCell>
            <TableCell>
              <span className="text-sm font-medium">{user.imageUrl}</span>
            </TableCell>
            <TableCell>
              <span className="text-sm font-medium">{user.email}</span>
            </TableCell>
            <TableCell>
              <span className="text-sm font-medium">{user.phoneNumber}</span>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </>
  );
};

export default CustomerTable;
