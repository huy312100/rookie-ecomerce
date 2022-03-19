import { useContext, useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { SidebarContext } from '../context/SidebarContext';
import ProductServices from '../services/ProductServices';
import { notifyError, notifySuccess } from '../utils/toast';

const useProductSubmit = (id) => {
  const [imageUrl, setImageUrl] = useState('');
  // const [tag, setTag] = useState([]);
  const { isDrawerOpen, closeDrawer, setIsUpdate } = useContext(SidebarContext);

  const {
    register,
    handleSubmit,
    setValue,
    clearErrors,
    formState: { errors },
  } = useForm();

  const onSubmit = (data) => {
    const productData = {
      ...(id) && {Id:id},
      Name: data.name,
      Price: data.price,
      Description: data.description,
      CategoryId:data.categoryid,
      BrandId:data.brandid,
      Image: imageUrl,
    };
    
    if (id) {
      
      ProductServices.updateProduct(productData)
        .then((res) => {
          console.log(res);
          setIsUpdate(true);
          notifySuccess(res.message);
        })
        .catch((err) => notifyError(err.message));
      closeDrawer();
    } else {
      ProductServices.addProduct(productData)
        .then((res) => {
          setIsUpdate(true);
          notifySuccess(res.message);
        })
        .catch((err) => console.log(err));
      closeDrawer();
    }
  };

  useEffect(() => {
    if (!isDrawerOpen) {
      setValue('id');
      setValue('name');
      setValue('price');
      setValue('description');
      setValue('categoryid');
      setValue('brandid');
      setImageUrl('');

      clearErrors('id');
      clearErrors('name');
      clearErrors('price');
      clearErrors('description');

      return;
    }

    if (id) {
      ProductServices.getProductById(id)
        .then((res) => {
          console.log(res);
          if (res) {
            setValue('id', res.id);
            setValue('name', res.name);
            setValue('price', res.price);
            setValue('description', res.description);
            setValue('categoryid', res.category.id);
            setValue('brandid', res.brand.id);
            // setTag(JSON.parse(res.tag));
            setImageUrl(res.images[0].imageUrl);
          }
        })
        .catch((err) => {
          notifyError('Server error!');
        });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id, setValue, isDrawerOpen]);

  // useEffect(() => {
  //   setChildren(watch('children'));
  // }, [watch, children]);

  return {
    register,
    handleSubmit,
    onSubmit,
    errors,
    imageUrl,
    setImageUrl,
  };
};

export default useProductSubmit;
