import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useContext } from 'react';

import GlobalStateContext from '@/components/Context/GlobalStateContext';
import { signOut, twitterSignIn } from '@/lib/firebase';

const TwitterAuthButton = () => {
  const { globalState, setGlobalState } = useContext(GlobalStateContext);

  const onClick = async () => {
    setGlobalState({ ...globalState, isLoading: true });

    if (globalState.currentUser) {
      await signOut();
    } else {
      await twitterSignIn();
    }
  };

  return (
    <button className='flex-none h-8 bg-blue-400 rounded-2xl' onClick={async () => await onClick()}>
      <div className='flex gap-x-3 px-3'>
        <div>
          <FontAwesomeIcon icon={faTwitter} className='text-white' />
        </div>
        <span>{globalState.currentUser ? 'Log out' : 'Twitter Log In'}</span>
      </div>
    </button>
  );
};

export default TwitterAuthButton;

// inline gap-x-3 py-2 px-3
