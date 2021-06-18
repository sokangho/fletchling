import Image from 'next/image';
import { useContext } from 'react';

import TimelineContext from '@/components/Context/TimelineContext';
import HeartIcon from '@/components/HeartIcon';
import VerifiedBadge from '@/components/TwitterVerifiedBadge';
import TwitterUser from '@/interfaces/TwitterUser';

interface Props {
  user: TwitterUser;
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
      <div className='flex gap-x-2'>
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

        <HeartIcon isSaved={isSaved()} onClick={handleOnClick} />
      </div>
    </div>
  );
};

export default UserResult;
