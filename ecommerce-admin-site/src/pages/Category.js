import React, { useContext } from 'react';
import {
  Table,
  TableHeader,
  TableCell,
  TableFooter,
  TableContainer,
  Button,
  Input,
  Card,
  CardBody,
  Pagination,
} from '@windmill/react-ui';
import { FiPlus } from 'react-icons/fi';

// import useAsync from '../hooks/useAsync';
// import useFilter from '../hooks/useFilter';
import NotFound from '../components/table/NotFound';
// import Loading from '../components/preloader/Loading';
import { SidebarContext } from '../context/SidebarContext';
import PageTitle from '../components/Typography/PageTitle';
// import CategoryServices from '../services/CategoryServices';
// import CategoryTable from '../components/category/CategoryTable';
import SelectCategory from '../components/form/SelectCategory';
// import MainDrawer from '../components/drawer/MainDrawer';
// import CategoryDrawer from '../components/drawer/CategoryDrawer';

const Category = () => {
  const { toggleDrawer } = useContext(SidebarContext);
  // const { data, loading } = useAsync(CategoryServices.getAllCategory);

  // const {
  //   categoryRef,
  //   setFilter,
  //   handleChangePage,
  //   totalResults,
  //   resultsPerPage,
  //   dataTable,
  //   serviceData,
  //   handleSubmitCategory,
  // } = useFilter(data);

  return (
    <>
      <PageTitle>Category</PageTitle>
      
      <Card className="min-w-0 shadow-xs overflow-hidden bg-white dark:bg-gray-800 mb-5">
        <CardBody>
          <form
              // onSubmit={handleSubmitCategory}
              className="py-3 grid gap-4 lg:gap-6 xl:gap-6 md:flex xl:flex"
            >
              <div className="flex-grow-0 md:flex-grow lg:flex-grow xl:flex-grow">
                <Input
                  // ref={categoryRef}
                  className="border h-12 text-sm focus:outline-none block w-full bg-gray-100 border-transparent focus:bg-white"
                  type="search"
                  name="search"
                  placeholder="Search by category type"
                />
                <button
                  type="submit"
                  className="absolute right-0 top-0 mt-5 mr-1"
                />
            </div>

            <div className="flex-grow-0 md:flex-grow lg:flex-grow xl:flex-grow">
              <SelectCategory />
            </div>

            <div className="w-full md:w-56 lg:w-56 xl:w-56">
              <Button className="w-full rounded-md h-12">
                <span className="mr-3">
                  <FiPlus />
                </span>
                Add Category
              </Button>
            </div>
          </form>
        </CardBody>
      </Card>

      <TableContainer className="mb-8">
          <Table>
            <TableHeader>
              <tr>
                <TableCell>ID</TableCell>
                <TableCell>Name</TableCell>
                <TableCell>Decription</TableCell>
              </tr>
            </TableHeader>
            {/* <CategoryTable categories={dataTable} /> */}
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
    </>
  );
};

export default Category;
