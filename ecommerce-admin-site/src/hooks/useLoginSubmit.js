import Cookies from 'js-cookie';
import { useContext, useState } from 'react';
import { useForm } from 'react-hook-form';
import { useHistory } from 'react-router-dom';
import { AdminContext } from '../context/AdminContext';
import AuthenServices from '../services/AuthenServices';
// import { notifyError, notifySuccess } from '../utils/toast';

const useLoginSubmit = () => {
  const [loading, setLoading] = useState(false);
  const { dispatch } = useContext(AdminContext);
  const history = useHistory();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const onSubmit = ({ username, password }) => {
    setLoading(true);

    console.log(username, password);

    AuthenServices.loginAdmin({ username, password })
        .then((res) => {
          if (res) {
            setLoading(false);
            // notifySuccess('Login Success!');
            dispatch({ type: 'USER_LOGIN', payload: res });
            Cookies.set('adminInfo', JSON.stringify(res));
            history.replace('/');
          }
        })
        .catch((err) => {
          // notifyError(err ? err.response.data.message : err.message);
          setLoading(false);
        });
  };
  return {
    onSubmit,
    register,
    handleSubmit,
    errors,
    loading,
  };
};

export default useLoginSubmit;
