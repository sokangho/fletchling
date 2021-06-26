import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { logOut } from '@/lib/firebase';

const UserProfileOptionList = () => {
  const onClick = async () => {
    await logOut();
  };

  return (
    <button
      onClick={async () => {
        await onClick();
      }}
      className='flex gap-x-3 px-4 py-2 bg-twitter-dark absolute w-full border border-opacity-50 hover:bg-gray-600'>
      <div>
        <FontAwesomeIcon icon={faSignOutAlt} className='text-gray-400' />
      </div>
      <div className='text-gray-400'>Log Out</div>
    </button>
  );
};

export default UserProfileOptionList;
