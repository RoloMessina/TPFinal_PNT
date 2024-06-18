using TPFinal_PNT1.Models;

public interface ITurnera
{
    bool AltaUsuario(Usuario usuario);
    void AgregarTurno(Turno turno);
    void CancelarTurno(Turno turno);
    void ModificarTurno(Turno turno);
    void VisualizarTurno(Turno turno);
}
