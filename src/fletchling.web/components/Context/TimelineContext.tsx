import { createContext, Dispatch, SetStateAction } from 'react';

interface SavedTimelines {
  savedTimelines: string[];
  setSavedTimelines: Dispatch<SetStateAction<string[]>>;
}

const TimelineContext = createContext<SavedTimelines>({} as SavedTimelines);

export default TimelineContext;
