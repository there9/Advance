using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Infra
{
    //Menu 불러오기 버튼 눌렀을 때의 이벤트(pub Menu)(sub SelectTimeTable)
    public class LoadEvent : PubSubEvent<object> { }
    //Menu 저장하기 버튼 눌렀을 때의 이벤트(pub Menu)(sub SelectTimeTable, ShowTimeTable)
    public class SaveEvent : PubSubEvent<object> { }
    //Menu 시간표 이미지 버튼 눌렀을 떄의 이벤(pub Menu)(Sub ShowTimeTable)
    public class SaveTimeTableImgEvent : PubSubEvent<object> { }
    //SelectTiemTable 초기화 버튼 눌렀을 때의 이벤트 (pub SelectTimeTable)(Sub ShowTimeTable)
    public class ClearTableEvent : PubSubEvent<object> { }
    //SelectTiemTable 하나 삭제 버튼 눌렀을 때의 이벤트 (pub SelectTimeTable)(Sub ShowTimeTable)
    public class DeleteOneTableEvent : PubSubEvent<object> { }
    //SelectTiemTable 하나 삽입 버튼 또는 더블클릭 눌렀을 때의 이벤트 (pub SelectTimeTable)(Sub ShowTimeTable)
    public class InsertOneTableEvent : PubSubEvent<object> { }
    //SelectTiemTable 시간 충돌 이벤트 (pub TotalTimeTable)(Sub ShowTimeTable SelectTalble)
    public class TimeTableConflictEvent : PubSubEvent<object> { }
    //SelectTiemTable 하나 삽입 버튼 또는 더블클릭 눌렀을 때의 이벤트 (pub SelectTimeTable)(Sub ShowTimeTable)
    public class RequestAddItemEvent : PubSubEvent<object> { }


}
