import './App.css';
import RootLayout from './Layouts/RootLayout';
import { Button } from 'antd';

function App() {
  return (
    <RootLayout>
      <h1>Dashboard Content</h1>
      <Button type="primary">Click Me</Button>
    </RootLayout>
  );
}

export default App;
