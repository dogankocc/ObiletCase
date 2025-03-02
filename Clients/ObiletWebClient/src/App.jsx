import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Toolbar from '@mui/material/Toolbar'
import AppBar from '@mui/material/AppBar';
import TextField from '@mui/material/TextField';
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
      <>
          <AppBar position="static">
              <Toolbar variant="dense"></Toolbar>

          </AppBar>
          <TextField fullWidth label="fullWidth" id="fullWidth" />
    </>
  )
}

export default App
