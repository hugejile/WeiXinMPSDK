using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 用户点击media_id类型按钮后，微信服务器会将开发者填写的永久素材id对应的素材下发给用户，
    /// 永久素材类型可以是图片、音频、视频、图文消息。
    /// 请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
    /// </summary>
    public class SingleMediaButton : SingleButton
    {
        public SingleMediaButton()
            : base(ButtonType.media_id.ToString())
        {

        }
        public string name { get; set; }

        public string media_id { get; set; }
    }
}
