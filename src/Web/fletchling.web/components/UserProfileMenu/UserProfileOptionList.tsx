import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { signOut } from 'next-auth/client';

import TwitterUser from '@/interfaces/TwitterUser';

interface Props {
  user: TwitterUser;
}

const UserProfileOptionList = ({ user }: Props) => {
  return (
    <div className='flex flex-col mt-1 bg-twitter-dark absolute right-0 w-64 border border-opacity-50'>
      <button onClick={() => signOut()} className='flex gap-x-3 hover:bg-gray-600 w-full px-4 py-2'>
        <span>
          <FontAwesomeIcon icon={faSignOutAlt} className='text-gray-400' />
        </span>
        <span className='text-gray-400'>Sign out @{user.username}</span>
      </button>
    </div>
  );
};

export default UserProfileOptionList;
