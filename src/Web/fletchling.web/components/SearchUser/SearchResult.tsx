import Image from 'next/image';
import { useContext } from 'react';

import HeartButton from '@/components/Buttons/HeartButton';
import TimelineContext from '@/components/Context/TimelineContext';
import VerifiedBadge from '@/components/TwitterVerifiedBadge';
import TwitterUser from '@/interfaces/TwitterUser';

interface Props {
  user: TwitterUser;
}

const SearchResult = ({ user }: Props) => {
  const { savedTimelines, updateSavedTimelines } = useContext(TimelineContext);

  const handleOnClick = async () => {
    let arr: string[] = [];

    if (isSaved()) {
      arr = savedTimelines.filter((a) => a !== user.username);
    } else {
      arr = savedTimelines.concat(user.username);
    }

    await updateSavedTimelines(arr);
  };

  const isSaved = () => savedTimelines.includes(user.username);

  return (
    <div className='bg-gray-600 hover:bg-gray-700 p-2 border-1'>
      <div className='flex gap-x-2'>
        <div className='flex-shrink-0'>
          <Image
            src={user.profileImageUrl}
            alt='Profile Image'
            width={48}
            height={48}
            className='rounded-full'
          />
        </div>
        <div className='flex-grow'>
          <div className='flex flex-wrap gap-x-1'>
            <div className='font-semibold text-gray-200'>{user.displayName}</div>
            {user.verified && (
              <div className='flex-grow-0'>
                <VerifiedBadge />
              </div>
            )}
          </div>
          <div className='font-light text-gray-400'>@{user.username}</div>
        </div>

        <HeartButton
          isSaved={isSaved()}
          onClick={async () => {
            await handleOnClick();
          }}
        />
      </div>
    </div>
  );
};

export default SearchResult;
