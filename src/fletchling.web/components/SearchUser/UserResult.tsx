import { faHeart as farHeart } from '@fortawesome/free-regular-svg-icons';
import { faHeart as fasHeart } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Image from 'next/image';
import { useContext } from 'react';

import TimelineContext from '@/components/TimelineContext';
import VerifiedBadge from '@/components/TwitterVerifiedBadge';
import User from '@/interfaces/User';

interface Props {
  user: User;
}

const UserResult = ({ user }: Props) => {
  const { savedTimelines, setSavedTimelines } = useContext(TimelineContext);

  const handleOnClick = () => {
    let arr: string[] = [];

    if (isSaved()) {
      arr = savedTimelines.filter((a) => a !== user.username);
    } else {
      arr = savedTimelines.concat(user.username);
    }

    setSavedTimelines(arr);
  };

  const isSaved = () => savedTimelines.includes(user.username);

  return (
    <div className='bg-gray-300 hover:bg-gray-200 p-2 border-2'>
      <div className='flex flex-column gap-x-2'>
        <div className='flex-shrink-0'>
          <Image
            src={user.profileImageUrl}
            alt='Profile Image'
            width={48}
            height={48}
            className='rounded-3xl'
          />
        </div>
        <div className='flex-grow'>
          <div className='flex flex-wrap gap-x-1'>
            <div className='font-semibold'>{user.displayName}</div>
            <div className='flex-grow-0'>{user.verified && <VerifiedBadge />}</div>
          </div>
          <div className='font-light'>@{user.username}</div>
        </div>

        <button className='place-self-center text-2xl p-2 focus:outline-none text-red-500 hover:text-red-300'>
          <FontAwesomeIcon icon={isSaved() ? fasHeart : farHeart} onClick={handleOnClick} />
        </button>
      </div>
    </div>
  );
};

export default UserResult;
