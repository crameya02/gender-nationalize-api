import { useState } from 'react'
import './App.css'
import ProfileForm from "./components/ProfileForm";
function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div>


      <ProfileForm />
      </div>

    </>
  )
}

export default App
