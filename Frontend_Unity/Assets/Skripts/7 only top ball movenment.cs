using UnityEngine;

public class onlytopballmovement : MonoBehaviour
{
    public float speed = 5f;
    public float minY = 1.5f;
    public float maxY = 13f;
    public float minX = -9f;
    public float maxX = 9f;

    // Koordinaten f�r die Snapping-Positionen auf der X-Achse
    private float[] xSnapPoints = { -7.5f, -4.5f, -1.5f, 1.5f, 4.5f, 7.5f };

    // Koordinaten f�r die Snapping-Positionen auf der Y-Achse
    private float[] ySnapPoints = { 1.5f, 3.5f, 5.5f, 7.5f, 9.5f, 11.5f };

    // Index f�r die aktuelle Position im xSnapPoints-Array
    private int xSnapIndex = 0;

    // Index f�r die aktuelle Position im ySnapPoints-Array
    private int ySnapIndex = 0;

    // Index f�r die ausgew�hlte S�ule
    private int selectedColumnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Setze den Start-Index auf den Index der urspr�nglichen Position des Balls
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        // Falls die urspr�ngliche Position nicht in den definierten Snapping-Punkten liegt, setze den Index auf 0
        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // �berpr�fe, ob Pfeiltaste nach links gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Ausgew�hlte S�ule nach links verschieben (falls nicht bereits am Anfang)
            if (selectedColumnIndex > 0)
            {
                selectedColumnIndex--;
            }
        }

        // �berpr�fe, ob Pfeiltaste nach rechts gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Ausgew�hlte S�ule nach rechts verschieben (falls nicht bereits am Ende)
            if (selectedColumnIndex < xSnapPoints.Length - 1)
            {
                selectedColumnIndex++;
            }
        }

        // F�rbe die Seitenw�nde der ausgew�hlten S�ule
        ColorSelectedColumnWalls(selectedColumnIndex);

        // Nur bewegen, wenn der oberste Ball in der ausgew�hlten S�ule ist
        if (IsAtTopOfSelectedColumn())
        {
            // Bewegung auf der Y-Achse
            float verticalInput = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

            // Wenn die obere Pfeiltaste gedr�ckt wurde
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!IsAtCurrentYSnapPoint())
                {
                    JumpToNextYSnapPoint();
                }
                // Zum n�chsten Snapping-Punkt auf der Y-Achse springen lassen
            }
            // Wenn die untere Pfeiltaste gedr�ckt wurde
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Zum vorherigen Snapping-Punkt auf der Y-Achse springen lassen
                if (!IsAtCurrentYSnapPoint())
                {
                    JumpToPreviousYSnapPoint();
                }

                // Ball auf den ausgew�hlten Snapping-Punkt setzen
                Vector3 newPosition = new Vector3(GetNextXSnapPoint(), GetNextYSnapPoint(), transform.position.z);

                // Anwenden der Grenzen mithilfe von Mathf.Clamp
                float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
                float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);

                // Aktualisieren der Position des Balls nach Anwendung der Grenzen
                transform.position = new Vector3(clampedX, clampedY, newPosition.z);
            }
        }
    }

    // Funktion zum Abrufen des n�chsten Werts aus dem xSnapPoints-Array
    float GetNextXSnapPoint()
    {
        return xSnapPoints[xSnapIndex];
    }

    // Funktion zum Abrufen des n�chsten Werts aus dem ySnapPoints-Array
    float GetNextYSnapPoint()
    {
        return ySnapPoints[ySnapIndex];
    }

    // �berpr�fe, ob dieser Ball an der obersten Position in seiner Spalte ist
    bool IsAtTopOfSelectedColumn()
    {
        for (int i = 0; i < ySnapPoints.Length; i++)
        {
            // �berpr�fe, ob es einen Ball �ber diesem gibt
            if (i < ySnapIndex && Mathf.Approximately(transform.position.y, ySnapPoints[i]))
            {
                return false; // Nicht der oberste Ball in dieser S�ule
            }
            // �berpr�fe, ob es einen Ball unter diesem gibt
            else if (i > ySnapIndex && Mathf.Approximately(transform.position.y, ySnapPoints[i]))
            {
                return true; // Der oberste oder einzige Ball in dieser S�ule
            }
        }
        return true; // Der oberste oder einzige Ball in dieser S�ule
    }

    // �berpr�fe, ob der Ball an der aktuellen X-Snap-Position ist
    bool IsAtCurrentXSnapPoint()
    {
        return Mathf.Approximately(transform.position.x, GetNextXSnapPoint());
    }

    // �berpr�fe, ob der Ball an der aktuellen Y-Snap-Position ist
    bool IsAtCurrentYSnapPoint()
    {
        return Mathf.Approximately(transform.position.y, GetNextYSnapPoint());
    }

    // Funktion zum Springen zum vorherigen Snapping-Punkt auf der X-Achse
    void JumpToPreviousXSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        xSnapIndex = (xSnapIndex - 1 + xSnapPoints.Length) % xSnapPoints.Length;
    }

    // Funktion zum Springen zum n�chsten Snapping-Punkt auf der X-Achse
    void JumpToNextXSnapPoint()
    {
        // Zum n�chsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
        xSnapIndex = (xSnapIndex + 1) % xSnapPoints.Length;
    }

    // Funktion zum Springen zum vorherigen Snapping-Punkt auf der Y-Achse
    void JumpToPreviousYSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        ySnapIndex = (ySnapIndex - 1 + ySnapPoints.Length) % ySnapPoints.Length;
    }

    // Funktion zum Springen zum n�chsten Snapping-Punkt auf der Y-Achse
    void JumpToNextYSnapPoint()
    {
        // Zum n�chsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
        ySnapIndex = (ySnapIndex + 1) % ySnapPoints.Length;
    }

    // F�rbe die Seitenw�nde der ausgew�hlten S�ule
    void ColorSelectedColumnWalls(int columnIndex)
    {
        // Setze alle Seitenw�nde auf die Standardfarbe
        ResetWallColors();

        // W�hle die Seitenw�nde der ausgew�hlten S�ule aus und setze die Farbe
        GameObject leftWall = GameObject.Find("Sidewall " + (columnIndex + 1));
        GameObject rightWall = GameObject.Find("Sidewall " + (columnIndex + 2));

        if (leftWall != null)
        {
            leftWall.GetComponent<Renderer>().material.color = Color.red;
        }

        if (rightWall != null)
        {
            rightWall.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Setze die Farben aller Seitenw�nde auf die Standardfarbe
    void ResetWallColors()
    {
        for (int i = 1; i <= 7; i++)
        {
            GameObject wall = GameObject.Find("Sidewall " + i);
            if (wall != null)
            {
                wall.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}

