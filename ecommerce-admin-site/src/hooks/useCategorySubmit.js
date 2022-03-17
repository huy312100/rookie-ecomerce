import { useContext, useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { SidebarContext } from '../context/SidebarContext';
import CategoryServices from '../services/CategoryServices';
// import { notifyError, notifySuccess } from '../utils/toast';

const useCategorySubmit = (id) => {

  const { isDrawerOpen, closeDrawer, setIsUpdate } = useContext(SidebarContext);

  const {
    register,
    handleSubmit,
    setValue,
    clearErrors,
    formState: { errors },
  } = useForm();


  const onSubmit = ({ name, description }) => {
    
    const categoryData = {
      Name: name,
      // slug: slug,
      description: description,
    };

    if (id) {
      // CategoryServices.updateCategory(id, categoryData)
      //   .then((res) => {
      //     setIsUpdate(true);
      //     // notifySuccess(res.message);
      //   })
      //   .catch((err) => console.error(err));
      // closeDrawer();
    } else {
      CategoryServices.addCategory(categoryData)
        .then((res) => {
          setIsUpdate(true);
          console.log(res);
          // notifySuccess(res.message);
        })
        .catch((err) => console.log(err));
      closeDrawer();
    }
  };

  useEffect(() => {
    if (!isDrawerOpen) {
      setValue('name');
      // setValue("slug");
      setValue('description');
      clearErrors('name');
      // setValue("slug");
      clearErrors('description');
      return;
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id, setValue, isDrawerOpen]);
  return {
    register,
    handleSubmit,
    onSubmit,
    errors,
  };
};

export default useCategorySubmit;
