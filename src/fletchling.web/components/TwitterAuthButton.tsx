import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useContext } from 'react';

import AuthContext from '@/components/Context/AuthContext';
import { signOut, twitterSignIn } from '@/lib/firebase';

const TwitterAuthButton = () => {
  const { currentUser } = useContext(AuthContext);

  const onClick = () => {
    if (currentUser) {
      signOut();
    } else {
      twitterSignIn();
    }
  };

  return (
    <button className='flex gap-x-3 py-2 px-3 bg-blue-400 rounded-md' onClick={onClick}>
      <div>
        <FontAwesomeIcon icon={faTwitter} className='text-white' />
      </div>
      <span>{currentUser ? 'Log out' : 'Sign in with Twitter'}</span>
    </button>
  );
};

export default TwitterAuthButton;
