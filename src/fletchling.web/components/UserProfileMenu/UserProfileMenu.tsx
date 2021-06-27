import { faChevronDown } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Image from 'next/image';
import { useEffect, useRef, useState } from 'react';

import UserProfileOptionList from '@/components/UserProfileMenu/UserProfileOptionList';
// import VerifiedBadge from '@/components/TwitterVerifiedBadge';
import TwitterUser from '@/interfaces/TwitterUser';

interface Props {
  user: TwitterUser;
}

const UserProfileMenu = ({ user }: Props) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const menuOptionsRef = useRef<HTMLDivElement>(null);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  const hideMenuOptions = (e: MouseEvent) => {
    if (menuOptionsRef.current && !menuOptionsRef.current.contains(e.target as Node)) {
      setIsMenuOpen(false);
    }
  };

  useEffect(() => {
    document.addEventListener('mousedown', hideMenuOptions);

    return () => {
      document.removeEventListener('mousedown', hideMenuOptions);
    };
  }, []);

  return (
    <div className='relative' ref={menuOptionsRef}>
      <button
        onClick={toggleMenu}
        className='flex items-center w-48 gap-x-1 py-1 px-2 border border-opacity-0 hover:border-opacity-50'>
        <Image
          src={user.profileImageUrl}
          alt='Profile Image'
          width={35}
          height={35}
          className='rounded-full'
        />

        <div className='flex-1 mr-5 leading-none text-left truncate'>
          <div className='truncate text-gray-200'>
            <span className='font-semibold text-sm leading-none'>{user.displayName}</span>
            {/* {user.verified && (
          <span>
            <VerifiedBadge size={10} />
          </span>
        )} */}
          </div>
          <div className='truncate text-gray-400 font-light text-sm'>
            <span>@{user.username}</span>
          </div>
        </div>

        <div className='text-gray-400'>
          <FontAwesomeIcon icon={faChevronDown} />
        </div>
      </button>

      {isMenuOpen && <UserProfileOptionList />}
    </div>
  );
};

export default UserProfileMenu;
