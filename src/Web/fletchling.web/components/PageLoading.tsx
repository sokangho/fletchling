import { useContext } from 'react';
import ClipLoader from 'react-spinners/ClipLoader';

import GlobalStateContext from './Context/GlobalStateContext';

const PageLoading = () => {
  const {
    globalState: { isLoading }
  } = useContext(GlobalStateContext);

  if (isLoading) {
    return (
      <div className='absolute z-50 w-screen h-screen bg-black bg-opacity-60'>
        <div className='absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2'>
          <ClipLoader loading={true} size={50} color='silver' />
        </div>
      </div>
    );
  }

  return null;
};

export default PageLoading;
