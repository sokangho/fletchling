// import { FaHeart } from '@fortawesome/free-solid-svg-icons';
import { faHeart as farHeart } from '@fortawesome/free-regular-svg-icons';
// import { faHeart as fasHeart } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Image from 'next/image';
import Link from 'next/link';

import VerifiedBadge from '@/components/TwitterVerifiedBadge';
import User from '@/interfaces/User';

interface Props {
  user: User;
}

const UserResult = ({ user }: Props) => {
  return (
    <Link href={user.profileUrl}>
      <a target='_blank'>
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

            <button className='place-self-center text-2xl p-2 focus:outline-none hover:text-red-500'>
              <FontAwesomeIcon icon={farHeart} />
            </button>
          </div>
        </div>
      </a>
    </Link>
  );
};

export default UserResult;
