import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import RootLayout from './Layouts/RootLayout';
import Students from './Views/Students';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<RootLayout />}>
          <Route path="/" element={<div>Home Page</div>} />
          <Route path="/students" element={<Students />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
