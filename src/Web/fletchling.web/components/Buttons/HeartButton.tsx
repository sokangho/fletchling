import { faHeart as farHeart } from '@fortawesome/free-regular-svg-icons';
import { faHeart as fasHeart } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { ReactElement } from 'react';

interface Props {
  isSaved: boolean;
  onClick?: () => void;
}

const HeartButton = ({ isSaved, onClick }: Props): ReactElement => {
  return (
    <button className='text-2xl p-2 focus:outline-none text-red-500 hover:text-red-300'>
      <FontAwesomeIcon icon={isSaved ? fasHeart : farHeart} onClick={onClick} />
    </button>
  );
};

export default HeartButton;
