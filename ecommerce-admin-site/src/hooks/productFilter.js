import { useEffect, useMemo, useRef, useState } from 'react';

const useFilter = (data) => {
  const [currentPage, setCurrentPage] = useState(1);
  const [dataTable, setDataTable] = useState([]); //tableTable for showing on table according to filtering

  //pagination functionality start

  const resultsPerPage = 8;
  const totalResults = data.length;

  const handleChangePage = (p) => {
    setCurrentPage(p);
  };

  useEffect(() => {
    setDataTable(
      data.slice(
        (currentPage - 1) * resultsPerPage,
        currentPage * resultsPerPage
      )
    );
  }, [data, currentPage, resultsPerPage]);

  //pagination functionality end

  return {
    handleChangePage,
    totalResults,
    resultsPerPage,
    dataTable,
  };
};

export default useFilter;
