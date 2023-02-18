
import './App.css';
import React from 'react';
import AglomachineComponent from './Components/AglomachineComponent';

function App() {
  const [data, setData] = React.useState(null);
  const RenderAglomachines = ()=>{
    let results = [];
    data.forEach((element,index) => {
      results.push(<AglomachineComponent key={index} data={element}/>)
    });
    return results
  }
  React.useEffect(() => {
    fetch("/getmachines")
      .then((res) => res.json())
      .then((data) => {
        let convData = []
        data.forEach(element => {
          if(!convData[element.id-1])convData[element.id-1] = []
          convData[element.id-1][convData[element.id-1].length]= element;
        });
        setData(convData)
      });
  }, []);
  return (
    <div className="content-wrapper">
      {!data ? "Загрузка":RenderAglomachines()}
    </div>
  );
}


export default App;
