import React from 'react';
import '../css/schema.css'
import schemasvg from '../img/exg-schema.svg'
function ExgausterSchemaPage() {
    const [data, setData] = React.useState(null);
    const [state, setState] = React.useState({
        exid: ""
      });
      React.useEffect(() => {
        fetch("/getexdata?id="+state.exid)
          .then((res) => res.json())
          .then((data) => {
            setData(data)
          });
      }, []);

      return (
        <div className="schema-wrapper">
          {!data ? "Загрузка":
          <div>
            <img className='schema-svg' alt='schema' src={schemasvg}></img>
            <div className='schema-value' style={top}>{this.getBearingData()}</div>
          </div>
          }
        </div>
      );
}
export default ExgausterSchemaPage;