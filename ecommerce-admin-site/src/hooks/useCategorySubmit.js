import { useContext, useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { SidebarContext } from '../context/SidebarContext';
import CategoryServices from '../services/CategoryServices';
import { notifyError, notifySuccess } from '../utils/toast';

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
    console.log(id);
    const categoryData = {
      ...(id) && {Id:id},
      Name: name,
      // slug: slug,
      Description: description,
    };

    if (id) {
      CategoryServices.updateCategory(categoryData)
        .then((res) => {
          setIsUpdate(true);
          console.log(res);
          // notifySuccess(res.message);
        })
        .catch((err) => console.error(err));
      closeDrawer();
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
      setValue('description');
      clearErrors('name');
      clearErrors('description');
      return;
    }

    if (id) {
      CategoryServices.getCategoryById(id)
        .then((res) => {
          if (res) {
            setValue('name', res.name);
            setValue('description', res.description);
          }
        })
        .catch((err) => {
          notifyError('There is a server error!');
        });
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
