import React from 'react';
import '../css/schema.css'
import schemasvg from '../img/exg-schema.svg'
function ExgausterSchemaPage(props) {
    const [data, setData] = React.useState(null);

      React.useEffect(() => {
        fetch("/getexdata?id="+props.exid)
          .then((res) => res.json())
          .then((data) => {
            setData(data)
            console.log(data)
          });
      }, []);
      function getBearingData(bear_n){
        function parseKey(key){
            switch (key){
                case 'temperature':
                    return 'T,C';
                case 'va':
                    return 'О,мм/c';
                case 'vv':
                    return 'В,мм/c';
                case 'vh':
                    return 'Г,мм/c';
                default:
                    return '';
            }
        }
        let bearing_data;
        let result = [];
        data.bearings.forEach(element => {
            if(element.bearing_number == bear_n)bearing_data =element
        });
        for(let key in bearing_data){
            if(key==='temperature'||((key==='vh'||key==='vv'||key==='va')&(bear_n===1||bear_n===2||bear_n===7||bear_n===8))){
                result[result.length] = <div className='schema-key-value' key={key+bear_n}>
                    <div>{parseKey(key)}</div>
                    <div>{bearing_data[key]}</div></div>
            }
        }
        return result;
      }
      return (
        <div className="schema-wrapper">
          {!data ? "Загрузка":
          <div>
            <img className='schema-svg' alt='schema' src={schemasvg}></img>
            <div className='schema-bear-data' style={{top:'620px',left:'1420px'}}>{getBearingData(1)}</div>
            <div className='schema-bear-data' style={{top:'620px',left:'1080px'}}>{getBearingData(2)}</div>
            <div className='schema-bear-data' style={{top:'330px',left:'930px'}}>{getBearingData(3)}</div>
            <div className='schema-bear-data' style={{top:'330px',left:'755px'}}>{getBearingData(4)}</div>
            <div className='schema-bear-data' style={{top:'620px',left:'930px'}}>{getBearingData(5)}</div>
            <div className='schema-bear-data' style={{top:'620px',left:'755px'}}>{getBearingData(6)}</div>
            <div className='schema-bear-data' style={{top:'700px',left:'755px'}}>{getBearingData(7)}</div>
            <div className='schema-bear-data' style={{top:'605px',left:'290px'}}>{getBearingData(8)}</div>
            <div className='schema-bear-data' style={{top:'510px',left:'290px'}}>{getBearingData(9)}</div>

            <div className='schema-value' style={{position:'absolute',top:'297px',left:'607px'}}>{data.exgauster.gas_underpressure_before}</div>
            <div className='schema-value' style={{position:'absolute',top:'320px',left:'607px'}}>?</div>

            <div className='schema-value' style={{position:'absolute',top:'260px',left:'1347px'}}>{data.exgauster.oil_temperature_after}</div>
            <div className='schema-value' style={{position:'absolute',top:'185px',left:'1210px'}}>{data.exgauster.oil_temperature_before}</div>
            <div className='schema-value' style={{position:'absolute',top:'90px',left:'1300px'}}>{data.exgauster.water_temperature_after}</div>
            <div className='schema-value' style={{position:'absolute',top:'90px',left:'1390px'}}>{data.exgauster.water_temperature_before}</div>

            <div className='schema-value' style={{position:'absolute',top:'400px',left:'1385px'}}>{data.exgauster.rotor_current}</div>
            <div className='schema-value' style={{position:'absolute',top:'425px',left:'1385px'}}>{data.exgauster.stator_current}</div>
            <div className='schema-value' style={{position:'absolute',top:'448px',left:'1385px'}}>{data.exgauster.rotor_voltage}</div>
            <div className='schema-value' style={{position:'absolute',top:'472px',left:'1385px'}}>{data.exgauster.stator_voltage}</div>
          </div>
          }
        </div>
      );
}
export default ExgausterSchemaPage;