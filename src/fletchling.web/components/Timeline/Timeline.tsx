import { useContext, useState } from 'react';
import { Timeline as TimelineWidget } from 'react-twitter-widgets';

import HeartButton from '@/components/Buttons/HeartButton';
import TimelineContext from '@/components/Context/TimelineContext';

interface Props {
  username: string;
}

const Timeline = ({ username }: Props) => {
  const [isLoaded, setIsLoaded] = useState(false);
  const { savedTimelines, updateSavedTimelines } = useContext(TimelineContext);

  const onTimeLineLoaded = () => {
    setIsLoaded(true);
  };

  const removeTimeLine = () => {
    const arr = savedTimelines.filter((a) => a !== username);
    updateSavedTimelines(arr);
  };

  return (
    <div>
      {isLoaded && (
        <div className='flex justify-between px-2 bg-twitter-dark'>
          <span className='my-auto text-gray-200'>@{username}</span>
          <span>
            <HeartButton isSaved={true} onClick={removeTimeLine} />
          </span>
        </div>
      )}

      <TimelineWidget
        dataSource={{ sourceType: 'profile', screenName: username }}
        options={{ chrome: 'noheader, nofooter', theme: 'dark', width: '350', height: '500' }}
        onLoad={onTimeLineLoaded}
      />
    </div>
  );
};

export default Timeline;
