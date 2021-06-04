import React, { useState } from 'react';

interface FormObject {
  [key: string]: string | number;
}

const useForm = (initialState: FormObject) => {
  const [values, setValues] = useState(initialState);

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setValues({ ...values, [e.target.name]: e.target.value });
  }

  return { values, handleChange };
};

export default useForm;
