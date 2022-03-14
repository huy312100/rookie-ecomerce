import React from 'react';
import {
  Table,
  TableHeader,
  TableCell,
  TableFooter,
  TableContainer,
  Input,
  Card,
  CardBody,
  Pagination,
} from '@windmill/react-ui';

// import useAsync from '../hooks/useAsync';
// import useFilter from '../hooks/useFilter';
import NotFound from '../components/table/NotFound';
// import UserServices from '../services/UserServices';
// import Loading from '../components/preloader/Loading';
import PageTitle from '../components/Typography/PageTitle';
// import CustomerTable from '../components/customer/CustomerTable';

const Customers = () => {
  // const { data, loading } = useAsync(UserServices.getAllUsers);

  // const {
  //   userRef,
  //   handleChangePage,
  //   totalResults,
  //   resultsPerPage,
  //   dataTable,
  //   serviceData,
  //   handleSubmitUser,
  // } = useFilter(data);

  return (
    <>
      <PageTitle>Customers</PageTitle>    
      <Card className="min-w-0 shadow-xs overflow-hidden bg-white dark:bg-gray-800 mb-5">
        <CardBody>
          <form
            className="py-3 grid gap-4 lg:gap-6 xl:gap-6 md:flex xl:flex"
          >
            <div className="flex-grow-0 md:flex-grow lg:flex-grow xl:flex-grow">
              <Input
                // ref={userRef}
                className="border h-12 text-sm focus:outline-none block w-full bg-gray-100 border-transparent focus:bg-white"
                type="search"
                name="search"
                placeholder="Search by name/email/phone"
              />
              <button
                type="submit"
                className="absolute right-0 top-0 mt-5 mr-1"
              />
            </div>
          </form>
        </CardBody>
      </Card> 

      <TableContainer className="mb-8">
        <Table>
          <TableHeader>
            <tr>
            <TableCell>ID</TableCell>
                <TableCell>Username</TableCell>
                <TableCell>First Name</TableCell>
                <TableCell>Last Name</TableCell>
                <TableCell>Email</TableCell>
                <TableCell>Phone</TableCell>
            </tr>
          </TableHeader>
        </Table>
        <TableFooter>
            <Pagination
              totalResults={20}
              resultsPerPage={8}
              onChange={() => {}}
              label="Table navigation"
            />
          </TableFooter>
      </TableContainer>
      {/* <NotFound title="Customer" /> */}
    </>
  );
};

export default Customers;
