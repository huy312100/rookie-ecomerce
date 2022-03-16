import React from 'react';
import Scrollbars from 'react-custom-scrollbars';
import { Textarea, Select } from '@windmill/react-ui';
// import ReactTagInput from '@pathofdev/react-tag-input';

import Title from '../form/Title';
// import Error from '../form/Error';
import LabelArea from '../form/LabelArea';
import InputArea from '../form/InputArea';
import InputValue from '../form/InputValue';
// import SelectOption from '../form/SelectOption';
import DrawerButton from '../form/DrawerButton';
import Uploader from '../image-uploader/Uploader';
// import ChildrenCategory from '../category/ChildrenCategory';
import CategoryList from '../category/CategoryList';
import useProductSubmit from '../../hooks/useProductSubmit';

const ProductDrawer = ({ id }) => {
  const {
    register,
    // watch,
    // handleSubmit,
    // onSubmit,
    // errors,
    // imageUrl,
    // setImageUrl,
    // tag,
    // setTag,
  } = useProductSubmit(id);

  return (
    <>
      <div className="w-full relative p-6 border-b border-gray-100 bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-300">
        {id ? (
          <Title
            title="Update Product"
            description="Updated your product and necessary information from here"
          />
        ) : (
          <Title
            title="Add Product"
            description="Add your product and necessary information from here"
          />
        )}
      </div>
      <Scrollbars className="w-full md:w-7/12 lg:w-8/12 xl:w-8/12 relative dark:bg-gray-700 dark:text-gray-200">
        <form className="block">
          <div className="px-6 pt-8 flex-grow w-full h-full max-h-full pb-40 md:pb-32 lg:pb-32 xl:pb-32">
            
            <div className="grid grid-cols-6 gap-3 md:gap-5 xl:gap-6 lg:gap-6 mb-6">
              <LabelArea label="Product Image" />
              <div className="col-span-8 sm:col-span-4">
                <Uploader />
              </div>
            </div>

            <div className="grid grid-cols-6 gap-3 md:gap-5 xl:gap-6 lg:gap-6 mb-6">
              <LabelArea label="Product Name" />
              <div className="col-span-8 sm:col-span-4">
                <InputArea
                  register={register}
                  required="false"
                  label="Product Name"
                  name="name"
                  type="text"
                  placeholder="Name"
                />
                {/* <Error errorName={errors.sku} /> */}
              </div>
            </div>

            <div className="grid grid-cols-6 gap-3 md:gap-5 xl:gap-6 lg:gap-6 mb-6">
              <LabelArea label="Product Price" />
              <div className="col-span-8 sm:col-span-4">
                <InputArea
                  register={register}
                  required="false"
                  label="Product Price"
                  name="price"
                  type="text"
                  placeholder="Price"
                />
                {/* <Error errorName={errors.sku} /> */}
              </div>
            </div>

            <div className="grid grid-cols-6 gap-3 md:gap-5 xl:gap-6 lg:gap-6 mb-6">
              <LabelArea label="Product Description" />
              <div className="col-span-8 sm:col-span-4">
                <Textarea
                  className="border text-sm focus:outline-none block w-full bg-gray-100 border-transparent focus:bg-white"
                  {...register('description', {
                    required: 'Description is required!',
                    minLength: {
                      value: 20,
                      message: 'Minimum 20 character!',
                    },
                  })}
                  name="description"
                  placeholder="Details"
                  rows="4"
                  spellCheck="false"
                />
              </div>
            </div>

            <div className="grid grid-cols-6 gap-3 md:gap-5 xl:gap-6 lg:gap-6 mb-6">
              <LabelArea label="Category" />
              <div className="col-span-8 sm:col-span-4">
                <Select
                  className="border h-12 text-sm focus:outline-none block w-full bg-gray-100 dark:bg-white border-transparent focus:bg-white"
                  name="parent"
                  {...register('parent', {
                    required: 'Product parent category is required!',
                  })}
                >
                  <option value="" defaultValue hidden>
                    Select category
                  </option>
                  <CategoryList />
                </Select>
                {/* <Error errorName={errors.parent} /> */}
              </div>
            </div>
          </div>

          <DrawerButton id={id} title="Product" />

        </form>
      </Scrollbars>
    </>
  );
};

export default React.memo(ProductDrawer);
