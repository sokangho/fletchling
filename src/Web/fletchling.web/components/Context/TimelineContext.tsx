import { createContext } from 'react';

interface SavedTimelines {
  savedTimelines: string[];
  updateSavedTimelines: (savedTimelines: string[]) => Promise<void>;
}

const TimelineContext = createContext<SavedTimelines>({} as SavedTimelines);

export default TimelineContext;
